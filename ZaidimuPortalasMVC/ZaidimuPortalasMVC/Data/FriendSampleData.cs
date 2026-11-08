using ZaidimuPortalasMVC.Models;

namespace ZaidimuPortalasMVC.Data;

// Prototype only: each signed-in account has its own sample list.
// Changes last until the application restarts; no database is changed.
public static class FriendSampleData
{
    private static readonly object Sync = new();
    private static readonly Dictionary<string, List<FriendViewModel>> Lists = new();

    public static IReadOnlyList<FriendViewModel> GetFriends(string userId)
    {
        lock (Sync)
        {
            return GetOrCreate(userId).ToArray();
        }
    }

    public static bool Remove(string userId, int friendId)
    {
        lock (Sync)
        {
            return GetOrCreate(userId).RemoveAll(friend => friend.Id == friendId) > 0;
        }
    }

    private static List<FriendViewModel> GetOrCreate(string userId)
    {
        if (!Lists.TryGetValue(userId, out var friends))
        {
            friends = new()
            {
                new(1, "Jonas", "Gamer456"),
                new(2, "Mantas", "DarkSoul"),
                new(3, "Ieva", "PixelFox")
            };
            Lists.Add(userId, friends);
        }
        return friends;
    }
}
