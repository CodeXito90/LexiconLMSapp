﻿using Bogus;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Infrastructure.Data;

public static class SeedData
{
    private static UserManager<ApplicationUser> userManager = null!;
    private static RoleManager<IdentityRole> roleManager = null!;
    private const string adminRole = "Teacher";
    private const string studentRole = "Student";

    public static async Task SeedDataAsync(this IApplicationBuilder builder)
    {
        using (var scope = builder.ApplicationServices.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var db = serviceProvider.GetRequiredService<LmsContext>();

            if (await db.Users.AnyAsync()) return;

            userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>() ?? throw new ArgumentNullException(nameof(userManager));
            roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>() ?? throw new ArgumentNullException(nameof(roleManager));

            try
            {
                var roleNames = new[] { adminRole, studentRole };
                await CreateRolesAsync(roleNames);
                await GenerateUsersAsync(5);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    private static async Task CreateRolesAsync(string[] roleNames)
    {
        foreach (var roleName in roleNames)
        {
            if (await roleManager.RoleExistsAsync(roleName)) continue;
            var role = new IdentityRole { Name = roleName };
            var result = await roleManager.CreateAsync(role);

            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
        }
    }

    private static async Task GenerateUsersAsync(int nrOfUsers)
    {
        var faker = new Faker<ApplicationUser>("sv").Rules((f, e) =>
        {
            e.Email = f.Person.Email;
            e.UserName = f.Person.Email;
            e.FirstName = f.Person.FirstName;
            e.LastName = f.Person.LastName;
            e.CourseId = 1;
            e.Role = "Student";
            e.EmailConfirmed = true; // Ensure email is confirmed
        });

        var users = faker.Generate(nrOfUsers);

        // Set the first user as a teacher
        users[0].Role = "Teacher";

        var passWord = "BytMig123!";
        if (string.IsNullOrEmpty(passWord))
            throw new Exception("Password not found");

        foreach (var user in users)
        {
            var result = await userManager.CreateAsync(user, passWord);
            if (!result.Succeeded)
            {
                var errors = string.Join("\n", result.Errors.Select(e => e.Description));
                Console.WriteLine($"Failed to create user {user.Email}: {errors}");
                throw new Exception(errors);
            }
        }

        for (var i = 0; i < users.Count; i++)
        {
            IdentityResult result;
            if (i == 0)
            {
                result = await userManager.AddToRoleAsync(users[i], adminRole);
            }
            else
            {
                result = await userManager.AddToRoleAsync(users[i], studentRole);
            }
            if (!result.Succeeded)
            {
                var errors = string.Join("\n", result.Errors.Select(e => e.Description));
                Console.WriteLine($"Failed to add role to user {users[i].Email}: {errors}");
                throw new Exception(errors);
            }
        }
    }
}


