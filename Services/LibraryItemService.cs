using System.Collections.Generic;
using LibraryManagement.Common;
using LibraryManagement.Common.Models;
using LibraryManagement.Interfaces;

namespace LibraryManagement.Services;

public class LibraryItemService : ILibraryItemService
{
    private readonly Dictionary<int, LibraryItem> _items = new();

    public OperationResult AddItem(LibraryItem item)
    {
        if (item == null)
            return new OperationResult(false, "Material is empty.");

        if (_items.ContainsKey(item.Id))
            return new OperationResult(false, "This ID already exists.");

        _items.Add(item.Id, item);

        return new OperationResult(true, "Material added successfully.");
    }

    public LibraryItem GetItemById(int id)
    {
        if (_items.TryGetValue(id, out LibraryItem item))
            return item;

        return null;
    }

    public LibraryItem[] GetAllItems()
    {
        LibraryItem[] result = new LibraryItem[_items.Count];

        _items.Values.CopyTo(result, 0);

        return result;
    }

    public LibraryItem[] SearchItems(string titlePart)
    {
        List<LibraryItem> result = new();

        foreach (LibraryItem item in _items.Values)
        {
            if (item.Title.ToLower().Contains(titlePart.ToLower()))
            {
                result.Add(item);
            }
        }

        return result.ToArray();
    }

    public LibraryItem[] SearchItems(int publishedYear)
    {
        List<LibraryItem> result = new();

        foreach (LibraryItem item in _items.Values)
        {
            if (item.PublishedYear == publishedYear)
            {
                result.Add(item);
            }
        }

        return result.ToArray();
    }

    public OperationResult UpdateItem(int id, string newTitle, int newPublishedYear)
    {
        LibraryItem item = GetItemById(id);

        if (item == null)
            return new OperationResult(false, "Material not found.");

        item.UpdateInfo(newTitle, newPublishedYear);

        return new OperationResult(true, "Material updated successfully.");
    }

    public OperationResult DeleteItem(int id)
    {
        LibraryItem item = GetItemById(id);

        if (item == null)
            return new OperationResult(false, "Material not found.");

        if (!item.IsAvailable)
            return new OperationResult(false, "Cannot delete a borrowed material.");

        _items.Remove(id);

        return new OperationResult(true, "Material deleted successfully.");
    }
}
