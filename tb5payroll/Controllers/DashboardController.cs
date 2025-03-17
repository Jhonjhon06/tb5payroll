using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;


namespace tb5payroll.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetSheets()
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                var sheets = new List<string>();

                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        foreach (var sheet in package.Workbook.Worksheets)
                        {
                            sheets.Add(sheet.Name);
                        }
                    }
                }

                return Json(sheets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetData(string sheetName)
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                var employees = new List<Employee>();

                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[sheetName];
                        if (worksheet == null)
                        {
                            return NotFound("Sheet not found.");
                        }

                        // Start reading from row 5 (A5 for IDs, B5 for names)
                        int startRow = 5;
                        for (int row = startRow; row <= worksheet.Dimension.End.Row; row++)
                        {
                            var id = worksheet.Cells[row, 1].Text; // Column A (ID)
                            var name = worksheet.Cells[row, 2].Text; // Column B (Name)

                            // Add to the list if both ID and Name are not empty
                            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(name))
                            {
                                employees.Add(new Employee
                                {
                                    Id = id,
                                    Name = name,
                                    Department = "", // Add other fields if needed
                                    Holiday = 0,
                                    Overtime = 0,
                                    HoursWorked = 0
                                });
                            }
                        }
                    }
                }

                return Json(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    public class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public int Holiday { get; set; }
        public int Overtime { get; set; }
        public int HoursWorked { get; set; }
    }
}