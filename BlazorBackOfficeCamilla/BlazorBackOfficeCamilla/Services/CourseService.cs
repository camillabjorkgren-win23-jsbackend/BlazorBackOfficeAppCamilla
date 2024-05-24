using BlazorBackOfficeCamilla.GraphQL;
using BlazorBackOfficeCamilla.Models;
using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using Newtonsoft.Json.Linq;

namespace BlazorBackOfficeCamilla.Services;
public class CourseService(GraphQLHttpClient client)
{
    private readonly GraphQLHttpClient _client = client;

    public async Task<List<CourseAdminCard>> RequestCourseCardAdmin()
    {
        var query = new GraphQLRequest
        {
            Query = @"
            {
                getCourses {
                    id
                    imageUri
                    title
                    authors {
                        name
                    }
                    prices {
                        price
                        discount
                    }
                }
            }"
        };

        var response = await _client.SendQueryAsync<CourseResponse>(query);
        if (response.Errors != null && response.Errors.Any())
        {
            // Handle errors if needed
            return null;
        }

        var courses = response.Data.GetCoursesAdmin;
        if (courses != null && courses.Any())
        {
            var courseCards = courses.Select(course =>
            {
                return new CourseAdminCard
                {
                    Id = course.Id,
                    Title = course.Title,
                    Author = course.Author,
                    Price = course.Price ?? 0,
                    DiscountPrice = course.DiscountPrice ?? 0,
                    ImageUri = course.ImageUri
                };
            }).ToList();

            return courseCards;

        }
        return null;
    }


    public async Task<CourseCreate> RequestCreateCourseAsync(Course course)
    {
        var request = new GraphQLRequest
        {
            Query = @"
                mutation ($input: CourseCreateRequestInput!) {
                    createCourse(input: $input) {
                        id
                        title
                    }
                }",
            Variables = new
            {
                input = new
                {
                    title = course.Title,
                    imageUri = course.ImageUri,
                    imageHeaderUri = course.ImageHeaderUri,
                    isBestseller = course.IsBestseller,
                    isDigital = course.IsDigital,
                    categories = course.Categories,
                    ingress = course.Ingress,
                    starRating = course.StarRating,
                    reviews = course.Reviews,
                    likes = course.Likes,
                    likesInProcent = course.LikesInProcent,
                    hours = course.Hours,
                    authors = course.Authors?.Select(a => new { name = a.Name, authorImage = a.AuthorImage }).ToList(),
                    prices = new
                    {
                        currency = course.Prices?.Currency,
                        price = course.Prices?.Price,
                        discount = course.Prices?.Discount
                    },
                    content = new
                    {
                        description = course.Content?.Description,
                        includes = course.Content?.Includes,
                        programDetails = course.Content?.ProgramDetails?.Select(pd => new { id = pd.Id, title = pd.Title, description = pd.Description }).ToList()
                    }
                }
            }
        };

        var response = await _client.SendMutationAsync<CourseResponse>(request);
        return response.Data.CreateCourse;
    }
    public async Task<Course> GetCourseByIdAsync(string id)
    {
        var request = new GraphQLRequest
        {
            Query = @"
            query ($id: String!) {
                getCourseById(id: $id) {
                    id
                    title
                    imageUri
                    imageHeaderUri
                    isBestseller
                    isDigital
                    categories
                    ingress
                    starRating
                    reviews
                    likes
                    likesInProcent
                    hours
                    authors {
                        name
                        authorImage
                    }
                    prices {
                        currency
                        price
                        discount
                    }
                    content {
                        description
                        includes
                        programDetails {
                            id
                            title
                            description
                        }
                    }
                }
            }",
            Variables = new { id }
        };

        var response = await _client.SendQueryAsync<CourseResponse>(request);
        return response.Data.GetCourseById;
    }

    public async Task<Course> RequestUpdateCourseAsync(Course course)
    {
        var request = new GraphQLRequest
        {
            Query = @"
            mutation ($input: CourseUpdateRequestInput!) {
                updateCourse(input: $input) {
                    id
                    title
                    imageUri
                    imageHeaderUri
                    isBestseller
                    isDigital
                    categories
                    ingress
                    starRating
                    reviews
                    likes
                    likesInProcent
                    hours
                    authors {
                        name
                        authorImage
                    }
                    prices {
                        currency
                        price
                        discount
                    }
                    content {
                        description
                        includes
                        programDetails {
                            id
                            title
                            description
                        }
                    }
                }
            }",
            Variables = new
            {
                input = new
                {
                    id = course.Id,
                    title = course.Title,
                    imageUri = course.ImageUri,
                    imageHeaderUri = course.ImageHeaderUri,
                    isBestseller = course.IsBestseller,
                    isDigital = course.IsDigital,
                    categories = course.Categories,
                    ingress = course.Ingress,
                    starRating = course.StarRating,
                    reviews = course.Reviews,
                    likes = course.Likes,
                    likesInProcent = course.LikesInProcent,
                    hours = course.Hours,
                    authors = course.Authors?.Select(a => new { name = a.Name, authorImage = a.AuthorImage }).ToList(),
                    prices = new
                    {
                        currency = course.Prices?.Currency,
                        price = course.Prices?.Price,
                        discount = course.Prices?.Discount
                    },
                    content = new
                    {
                        description = course.Content?.Description,
                        includes = course.Content?.Includes,
                        programDetails = course.Content?.ProgramDetails?.Select(pd => new { id = pd.Id, title = pd.Title, description = pd.Description }).ToList()
                    }
                }
            }
        };

        var response = await _client.SendMutationAsync<CourseResponse>(request);
        if (response.Data.UpdateCourse == null)
        {
            throw new Exception("Failed to update course");
        }
        return response.Data.UpdateCourse;
    }

    private class CourseResponse
    {
        public Course GetCourseById { get; set; }
        public Course UpdateCourse { get; set; }
        public CourseCreate CreateCourse { get; set; }
        public List<CourseAdminCard> GetCoursesAdmin { get; set; }
    }
}
