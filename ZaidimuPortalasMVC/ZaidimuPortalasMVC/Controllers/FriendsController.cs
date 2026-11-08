using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ZaidimuPortalasMVC.Data;
using ZaidimuPortalasMVC.Models;

namespace ZaidimuPortalasMVC.Controllers;

[Authorize]
[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
public class FriendsController(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await userManager.GetUserAsync(User);
        if (user is null) return Challenge();
        return View(FriendSampleData.GetFriends(user.Id));
    }

    [HttpGet]
    public async Task<IActionResult> Remove(int id)
    {
        var user = await userManager.GetUserAsync(User);
        if (user is null) return Challenge();
        var friend = FriendSampleData.GetFriends(user.Id).FirstOrDefault(f => f.Id == id);
        if (friend is null) return NotFound();
        return View(new RemoveFriendViewModel
        {
            Id = friend.Id, DisplayName = friend.DisplayName, Username = friend.Username
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(int id, [Bind("Password")] RemoveFriendViewModel model)
    {
        var user = await userManager.GetUserAsync(User);
        if (user is null) return Challenge();
        var friend = FriendSampleData.GetFriends(user.Id).FirstOrDefault(f => f.Id == id);
        if (friend is null) return NotFound();

        // Resolve the friend and account on the server, never from posted display fields.
        model.Id = friend.Id;
        model.DisplayName = friend.DisplayName;
        model.Username = friend.Username;

        if (ModelState.IsValid)
        {
            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                FriendSampleData.Remove(user.Id, friend.Id);
                TempData["FriendsSuccess"] = $"{friend.DisplayName} pašalintas iš draugų sąrašo.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(nameof(model.Password), result.IsLockedOut
                ? "Per daug nesėkmingų bandymų. Pabandykite vėliau."
                : "Slaptažodis neteisingas arba paskyra negali patvirtinti šio veiksmo.");
        }

        // Never send the submitted password back to the browser.
        model.Password = string.Empty;
        ModelState.SetModelValue(nameof(model.Password), string.Empty, string.Empty);
        return View(model);
    }
}
