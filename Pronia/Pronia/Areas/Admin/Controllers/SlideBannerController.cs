using Microsoft.AspNetCore.Mvc;
using Pronia.Database.Repository;
using Pronia.Helpers.Extentions;
using Pronia.Database.Models;
using Pronia.Database.Interfaces;
using Pronia.Database.ViewModels;
using Microsoft.AspNetCore.Authorization;
namespace Pronia.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SlideBannerController : Controller
{
    private readonly ISlideBannerRepository _slideBannerRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private const string FOLDER_NAME = "Upload/SlideBanner";
    public SlideBannerController(IWebHostEnvironment webHostEnvironment, ISlideBannerRepository slideBannerRepository)
    {
        _slideBannerRepository = slideBannerRepository;
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
    public async Task<IActionResult> Add(CreateSlideBannerViewModel slideBanner)
    {
        if (!ModelState.IsValid) return View(slideBanner);
        await _slideBannerRepository.Insert(slideBanner);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> UpdateAsync(int? id)
    {
        var slideBanner = await _slideBannerRepository.GetById(id);
        if (slideBanner is null && id is null) return RedirectToAction(nameof(NotFound));

        var model = new UpdateSlideBannerViewModel()
        {
            Title = slideBanner.Title,
            Description = slideBanner.Description,
            Offer = slideBanner.Offer
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int? id, UpdateSlideBannerViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        await _slideBannerRepository.Update(id, model);
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public IActionResult Delete(int? id)
    {
        _slideBannerRepository.RemoveById(id);
        return RedirectToAction(nameof(Index));
    }
}
