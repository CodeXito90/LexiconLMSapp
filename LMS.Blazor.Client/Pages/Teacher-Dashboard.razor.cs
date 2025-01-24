using LMS.Blazor.Client.Services;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

namespace LMS.Blazor.Client.Pages
{
    partial class Teacher_Dashboard
    {
        [Inject]
        private IApiService _apiService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        private string userName = "Loading...";
        private string activeTab = "courses"; // Default tab
        private List<CourseDto>? courseDto;
        private List<UserDto>? users;
        private List<ActivityDto>? activities;
        private List<ModuleDto> modules;
        private bool loaded;


        //protected override async Task OnInitializedAsync()
        //{
        //}

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender && !loaded)
            {
                loaded = true;
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;

                if (user.Identity?.IsAuthenticated == true)
                {
                    userName = $"{user.FindFirst("given_name")?.Value}, {user.FindFirst("Teacher")?.Value}";
                }

            await LoadDashboardData();
                StateHasChanged();

            }
        }

        private async Task LoadDashboardData()
        {
            // Keynote: Fetch courses, modules, activities, and users from the backend API dynamically with await methods. HTTPclient error at moment
            courseDto = (await _apiService.GetAsync<IEnumerable<CourseDto>>("api/courses"))?.ToList() ?? [];
            //modules = (await _apiService.GetAsync<IEnumerable<ModuleDto>>("api/modules"))?.ToList() ?? [];
            //activities = (await _apiService.GetAsync<IEnumerable<ActivityDto>>("api/activities"))?.ToList() ?? [];
            //users = (await _apiService.GetAsync<IEnumerable<UserDto>>("api/users"))?.ToList() ?? [];

            // Mock data for users
            users = new List<UserDto>
            {
                new UserDto { Id = "1", FirstName = "Anna", LastName = "Student", Email = "anna@example.com", Role = "Student" },
                new UserDto { Id = "2", FirstName = "John", LastName = "Teacher", Email = "john@example.com", Role = "Teacher" }
            };

                    // Mock data for activities
                    activities = new List<ActivityDto>
            {
                new ActivityDto { /* ActivityId = 1, */ Name = "Assignment 1", Description = "Complete Assignment 1", EndDate = DateTime.Today.AddDays(7) },
                new ActivityDto { /* ActivityId = 2, */ Name = "Quiz 1", Description = "Take Quiz 1", EndDate = DateTime.Today.AddDays(14) }
            };

        }

        private void SetActiveTabCourses()
        {
            activeTab = "courses";
        }

        private void SetActiveTabUsers()
        {
            activeTab = "users";
        }

        private void SetActiveTabActivities()
        {
            activeTab = "activities";
        }

        private void ShowCreateCourseModal()
        {
            // Implement logic to show a modal for creating a course
        }

        private void ShowEditCourseModal(CourseDto course)
        {
            // Implement logic to show a modal for editing a course
        }

        // private async Task DeleteCourse(int courseId)
        // {
        //     await _apiService.DeleteAsync($"api/courses/{courseId}");
        //     await LoadDashboardData();
        // }

        private void ViewModules(int courseId)
        {
            NavigationManager.NavigateTo($"/courses/{courseId}/modules");
        }

        private void ShowCreateUserModal()
        {
            // Implement logic to show a modal for creating a user
        }

        private void ShowEditUserModal(UserDto user)
        {
            // Implement logic to show a modal for editing a user
        }

        // private async Task DeleteUser(string userId)
        // {
        //     await _apiService.DeleteAsync($"api/users/{userId}");
        //     await LoadDashboardData();
        // }

        private void ShowCreateActivityModal()
        {
            // Implement logic to show a modal for creating an activity
        }

        private void ShowEditActivityModal(ActivityDto activity)
        {
            // Implement logic to show a modal for editing an activity
        }

        // private async Task DeleteActivity(int activityId)
        // {
        //     await _apiService.DeleteAsync($"api/activities/{activityId}");
        //     await LoadDashboardData();
        // }
    }
}
