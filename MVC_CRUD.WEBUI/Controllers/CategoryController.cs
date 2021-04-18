using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_CRUD.DATA.Model_Entity;
using System.Data.Entity;

namespace MVC_CRUD.WEBUI.Controllers
{
	public class CategoryController : Controller
	{
		private readonly NorthwindEntities DB;

		public CategoryController()
		{
			DB = new NorthwindEntities();
		}

		public ActionResult AllCategories()
		{
			return View(DB.Categories.ToList());
		}

		[HttpPost]
		public ActionResult AddUpdateCategory(string txtCategoryName, string txtCategoryDesc, int? hdnCategoryId)
		{
			// Add Category
			if (hdnCategoryId == null)
			{
				Category category = new Category
				{
					CategoryName = txtCategoryName,
					Description = txtCategoryDesc
				};
				DB.Categories.Add(category);
				DB.Entry(category).State = EntityState.Added;
				DB.SaveChanges();

			} // Update Category
			else
			{
				Category category = DB.Categories.FirstOrDefault(c => c.CategoryID == hdnCategoryId);
				category.CategoryName = txtCategoryName;
				category.Description = txtCategoryDesc;
				DB.Categories.Attach(category);
				DB.Entry(category).State = EntityState.Modified;
				DB.SaveChanges();
			}

			return RedirectToAction("AllCategories", "Category");
		}

		public ActionResult DeleteCategory(int id)
		{
			Category category = DB.Categories.FirstOrDefault(c => c.CategoryID == id);
			DB.Categories.Remove(category);
			DB.Entry(category).State = EntityState.Deleted;
			DB.SaveChanges();

			return RedirectToAction("AllCategories", "Category");
		}
	}
}
