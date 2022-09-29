using Microsoft.EntityFrameworkCore;
using ET_ASP;
using ET_ASP.Models;
namespace ET_ASP.Routes;

public static class CategoryEndpoints
{
    public static void MapcategoryEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/category", async (DataModel db) =>
        {
            return await db.Categorias.ToListAsync();
        })
        .WithName("GetAllcategorys");

        routes.MapGet("/api/category/{id}", async (Guid categoryID, DataModel db) =>
        {
            return await db.Categorias.FindAsync(categoryID)
                is category model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetcategoryById");

        routes.MapPut("/api/category/{id}", async (Guid categoryID, category category, DataModel db) =>
        {
            var foundModel = await db.Categorias.FindAsync(categoryID);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            //update model properties here

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("Updatecategory");

        routes.MapPost("/api/category/", async (category category, DataModel db) =>
        {
            db.Categorias.Add(category);
            await db.SaveChangesAsync();
            return Results.Created($"/categorys/{category.categoryID}", category);
        })
        .WithName("Createcategory");

        routes.MapDelete("/api/category/{id}", async (Guid categoryID, DataModel db) =>
        {
            if (await db.Categorias.FindAsync(categoryID) is category category)
            {
                db.Categorias.Remove(category);
                await db.SaveChangesAsync();
                return Results.Ok(category);
            }

            return Results.NotFound();
        })
        .WithName("Deletecategory");
    }
}
