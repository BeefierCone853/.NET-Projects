using CoreProjectAPI.Data;
using CoreProjectAPI.Models.Domain;
using CoreProjectAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoreProjectAPI.Repositories.Implementation;

public class ImageRepository(
    IHostEnvironment webHostEnvironment,
    IHttpContextAccessor httpContextAccessor,
    ApplicationDbContext dbContext) : IImageRepository
{
    public async Task<BlogImage> Upload(IFormFile imageFile, BlogImage blogImage)
    {
        var localPath = Path.Combine(
            webHostEnvironment.ContentRootPath,
            "Images",
            $"{blogImage.FileName}{blogImage.FileExtension}");
        await using var stream = new FileStream(localPath, FileMode.Create);
        await imageFile.CopyToAsync(stream);
        if (httpContextAccessor.HttpContext != null)
        {
            var request = httpContextAccessor.HttpContext.Request;
            var urlPath =
                $"{request.Scheme}://{request.Host}{request.PathBase}" +
                $"/Images/{blogImage.FileName}{blogImage.FileExtension}";
            blogImage.Url = urlPath;
        }

        await dbContext.BlogImages.AddAsync(blogImage);
        await dbContext.SaveChangesAsync();
        return blogImage;
    }

    public async Task<IEnumerable<BlogImage>> GetAllAsync()
    {
        return await dbContext.BlogImages.ToListAsync();
    }
}