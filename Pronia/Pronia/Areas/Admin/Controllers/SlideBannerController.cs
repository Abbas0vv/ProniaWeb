using Microsoft.AspNetCore.Mvc;
using Pronia.Database.Repository;
using Pronia.Helpers.Extentions;
using Pronia.Database.Models;


namespace Pronia.Areas.Admin.Controllers;

[Area("Admin")]
public class SlideBannerController : Controller
{
    private readonly SlideBannerRepository _slideBannerRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private const string FOLDER_NAME = "Upload/SlideBanner";
    public SlideBannerController(IWebHostEnvironment webHostEnvironment)
    {
        _slideBannerRepository = new SlideBannerRepository();
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var slideBanners = _slideBannerRepository.GetAll();
        var result = View(slideBanners);
        return result;
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(SlideBanner slideBanner)
    {
        if (!ModelState.IsValid) return View(slideBanner);
        if (slideBanner.File is null)
        {
            ModelState.AddModelError("File", "Please upload an image.");
            return View(slideBanner);
        }
        if (!slideBanner.File.ContentType.Contains("image"))
        {
            ModelState.AddModelError("File", "The uploaded file must be an image.");
            return View(slideBanner);
        }
        if (slideBanner.File.Length > 2097152)
        {
            ModelState.AddModelError("File", "The uploaded file size must not exceed 2 MB.");
            return View(slideBanner);
        }

        slideBanner.ImageUrl = slideBanner.File.CreateFile(_webHostEnvironment.WebRootPath, FOLDER_NAME);

        await _slideBannerRepository.Insert(slideBanner);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        var slideBanner = _slideBannerRepository.GetById(id);

        if (slideBanner is null) return RedirectToAction(nameof(NotFound));

        return View(slideBanner);
    }

    [HttpPost]
    public async Task<IActionResult> Update(SlideBanner slideBanner)
    {
        var existSlideBanner = _slideBannerRepository.GetById(slideBanner.Id);
        if (slideBanner is null) return RedirectToAction(nameof(NotFound));

        existSlideBanner.Title = slideBanner.Title;
        existSlideBanner.Description = slideBanner.Description;
        existSlideBanner.Offer = slideBanner.Offer;

        if (!slideBanner.File.ContentType.Contains("image"))
        {
            ModelState.AddModelError("File", "The uploaded file must be an image.");
            return View(slideBanner);
        }
        if (slideBanner.File.Length > 2097152)
        {
            ModelState.AddModelError("File", "The uploaded file size must not exceed 2 MB.");
            return View(slideBanner);
        }
        FileExtention.RemoveFile(_webHostEnvironment.WebRootPath, FOLDER_NAME, existSlideBanner.ImageUrl);

        var newImageUrl = slideBanner.File.CreateFile(_webHostEnvironment.WebRootPath, FOLDER_NAME);
        existSlideBanner.ImageUrl = newImageUrl;

        _slideBannerRepository.Update(existSlideBanner);
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var slideBanner = _slideBannerRepository.GetById(id);

        if (slideBanner is null) return RedirectToAction(nameof(NotFound));

        FileExtention.RemoveFile(_webHostEnvironment.WebRootPath, FOLDER_NAME, slideBanner.ImageUrl);
        _slideBannerRepository.RemoveById(id);
        return RedirectToAction(nameof(Index));
    }
}
