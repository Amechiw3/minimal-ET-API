using ET_ASP;
using ET_ASP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ET_ASP.Routes
{
    public static class TaskEndpoints
    {
	    public static void MaptaskEndpoints (this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/api/task", async (DataModel db) =>
            {
                return await db.Tareas.ToListAsync();
            })
            .WithName("GetAlltasks");

            routes.MapGet("/api/task/{taskID}", async (Guid taskID, DataModel db) =>
            {
                return await db.Tareas.FindAsync(taskID)
                    is task model
                        ? Results.Ok(model)
                        : Results.NotFound();
                
            })
            .WithName("GettaskById");

            routes.MapPut("/api/task/{taskID}", async (Guid taskID, task task, DataModel db) =>
            {
                var foundModel = await db.Tareas.FindAsync(taskID);

                if (foundModel is null)
                {
                    return Results.NotFound();
                }
                //update model properties here
                foundModel.categoryID = task.categoryID;
                foundModel.tittle = task.tittle;
                foundModel.prioTask = task.prioTask;
                foundModel.description = task.description;
                
                await db.SaveChangesAsync();

                return Results.NoContent();
            })   
            .WithName("Updatetask");

            routes.MapPost("/api/task/", async (task task, DataModel db) =>
            {
                db.Tareas.Add(task);
                await db.SaveChangesAsync();
                return Results.Created($"/tasks/{task.taskID}", task);
            })
            .WithName("Createtask");


            routes.MapDelete("/api/task/{taskID}", async (Guid taskID, DataModel db) =>
            {
                if (await db.Tareas.FindAsync(taskID) is task task)
                {
                    db.Tareas.Remove(task);
                    await db.SaveChangesAsync();
                    return Results.Ok(task);
                }

                return Results.NotFound();
            })
            .WithName("Deletetask");
        }
    }}
