using Demo_PRN231_SE1618.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo_PRN231_SE1618.Controllers
{
    public class HomeController : Controller
    {
        private readonly MySaleDBContext _context;
        public HomeController(MySaleDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //Lỗi: Phương thức ToList() sẽ thu thập các đối tượng dưới dạng List
            Category categories = _context.Categories.ToList(); 
            return View(categories);
        }
        [HttpPost]
        public IActionResult AddorUpdate(int category_id, int buttonValue)
        {
            //Lỗi: truyền vào một tham số dạng Integer nhưng lại sử dụng như một Object(*)
            if(!string.IsNullOrEmpty(category.CategoryName))
            {
                if(buttonValue == 1)
                {
                    //Lỗi: phương thức Add cần truyền vào một Object (**)
                    _context.Categories.Add(category);
                }
                else
                {
                    //(*)
                    var existingCategory = _context.Categories.Find(category.CategoryId);
                    if (existingCategory != null)
                    {
                        //(*)
                        existingCategory.CategoryName = category.CategoryName;
                    }
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        //Lỗi: truyền tham số vào nhưng không sử dụng
        public IActionResult Edit(int id)
        {
            return View();
        }
        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(p => p.CategoryId == id);
            if (category != null)
            {
                //Lỗi: Nếu danh mục Category(Foreign key) cần xóa được sử dụng trong bảng Product thì không thể xóa từ một phía
                // Xóa danh mục
                _context.Categories.Remove(category);

                // Lưu thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
