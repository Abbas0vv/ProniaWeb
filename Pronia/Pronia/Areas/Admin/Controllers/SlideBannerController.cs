using Microsoft.AspNetCore.Mvc;
using Pronia.Database.Models;
using Pronia.Database.Repository;

namespace Pronia.Areas.Admin.Controllers;

[Area("Admin")]
public class SlideBannerController : Controller
{
    private readonly SlideBannerRepository _slideBannerRepository;

    public SlideBannerController()
    {
        _slideBannerRepository = new SlideBannerRepository();
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
    public async Task<IActionResult> Add(string title, string description, string offer)
    {
        SlideBanner slideBanner = new SlideBanner(title, description, offer);
        await _slideBannerRepository.Insert(slideBanner);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        var slieBanner = _slideBannerRepository.GetById(id);

        if (slieBanner is null) return RedirectToAction(nameof(NotFound));

        return View(slieBanner);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, string title, string description, string offer)
    {
        var slideBanner = _slideBannerRepository.GetById(id);
        if (slideBanner is null) return RedirectToAction(nameof(NotFound));

        slideBanner.Title = title;
        slideBanner.Description = description;
        slideBanner.Offer = offer;

        _slideBannerRepository.Update(slideBanner);
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var slideBanner = _slideBannerRepository.GetById(id);

        if (slideBanner is null) return RedirectToAction(nameof(NotFound));

        _slideBannerRepository.RemoveById(id);
        return RedirectToAction(nameof(Index));
    }
}
