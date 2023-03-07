using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUD2.Models;
using MySqlConnector;
using System.Globalization;

namespace CRUD2.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly DbasecrudContext _context;
        

        public UsuariosController(DbasecrudContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
              return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }


        public static (int, int, int) Consultas()
        {

            string conexion = "Server=localhost;Database=dbasecrud;User ID=root;Password=root;Pooling=false;";
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();

            //string query = "Select * From contribuyentes Where Cedula = ?pId AND Nacio = ?Nci";
            string queryWork = "Select COUNT(*) From dbasecrud.usuarios where Rol=1";
            string queryEsp = "Select COUNT(*) From dbasecrud.usuarios where Rol=2";
            string queryMan = "Select COUNT(*) From dbasecrud.usuarios where Rol=3";

            MySqlCommand mycomandWork = new MySqlCommand(queryWork, conn);
            MySqlCommand mycomandEsp = new MySqlCommand(queryEsp, conn);
            MySqlCommand mycomandMan = new MySqlCommand(queryMan, conn);
            //mycomand.Parameters.AddWithValue("?pId", pId)

            var resultWork = mycomandWork.ExecuteScalar();
            var resultEsp = mycomandEsp.ExecuteScalar();
            var resultMan = mycomandMan.ExecuteScalar();

            conn.Close();

            var CantidadRegistrosWork = Convert.ToInt32(resultWork);
            var CantidadRegistrosEsp = Convert.ToInt32(resultEsp);
            var CantidadRegistrosMan = Convert.ToInt32(resultMan);

            //Console.WriteLine("Cantidad de Registros = "+ CantidadRegistros);

            return (CantidadRegistrosWork, CantidadRegistrosEsp, CantidadRegistrosMan);

        }



        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LastName,Email,Address,Phone,WorkingStartDate,Rol,Salary,LastRevision,IncreasesSalary,IncreasesDates")] Usuario usuario, IFormFile Picture)
        {
            var(countWork, countEsp, countMan) = Consultas();

            var totalcount = countWork + countEsp + countMan;

            if (usuario.Rol == "1" && (totalcount < 10 && countWork < 4))
            {
                if (Picture != null && Picture.Length > 0)
                {

                    using var memoryStream = new MemoryStream();

                    await Picture.CopyToAsync(memoryStream);
                    byte[] imageData = memoryStream.ToArray();
                    usuario.Picture = imageData;
                }
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            else if (usuario.Rol == "2" && (totalcount < 10 && countEsp < 4))
            {
                if (Picture != null && Picture.Length > 0)
                {

                    using var memoryStream = new MemoryStream();

                    await Picture.CopyToAsync(memoryStream);
                    byte[] imageData = memoryStream.ToArray();
                    usuario.Picture = imageData;
                }
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else if (usuario.Rol == "3" && (totalcount < 10 && countMan < 2))
            {
                if (Picture != null && Picture.Length > 0)
                {

                    using var memoryStream = new MemoryStream();

                    await Picture.CopyToAsync(memoryStream);
                    byte[] imageData = memoryStream.ToArray();
                    usuario.Picture = imageData;
                }
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("***************************** INFO *****************************");
                Console.WriteLine("COMPRUEBE LA CANTIDAD DE USUARIOS REGISTRADOS! (10 MAX: 4 WORK, 4 SPE, 2 MAN)");
                Console.WriteLine("****************************************************************");
                return View(usuario);
            }
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        public ActionResult ConvertirImagen(int id)
        {
            var imagen = _context.Usuarios.Find(id);

            if (imagen != null)
            {
                return File(imagen.Picture, "image/jpg");
            }
            return NotFound();

        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LastName,Email,Address,Phone,WorkingStartDate,Rol,Salary,LastRevision,IncreasesSalary,IncreasesDates")] Usuario usuario, IFormFile Picture)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (Picture != null && Picture.Length > 0)
            {

                using (var memoryStream = new MemoryStream())
                {
                    await Picture.CopyToAsync(memoryStream);
                    byte[] imageData = memoryStream.ToArray();
                    usuario.Picture = imageData;
                }

            }

            try
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(usuario.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'DbasecrudContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return _context.Usuarios.Any(e => e.Id == id);
        }


        //Method to calculate salary
        public async Task<IActionResult> CalculateSalary(int? id)
        {
            String message = "";

            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario != null)
            {

                DateTime dateStart = usuario.WorkingStartDate;
                DateTime dateCurrent = DateTime.Now.Date;
                DateTime dateEnd;
                int auxDate2;

                if (usuario.LastRevision == null)
                {
                    usuario.LastRevision = dateCurrent;
                    dateEnd = dateStart.AddDays(90); //Assuming 30-day months
                    var auxDate = DateTime.Compare(dateCurrent, dateEnd); // menor a cero: 1<2 , cero: 1=2 , mayor a cero: 1>2

                    usuario.IncreasesDates = dateCurrent.ToShortDateString();

                    if (auxDate >= 0)
                    {
                        String Rol = usuario.Rol;
                        float Salary = usuario.Salary;
                        float newSalary;

                        if (Rol == "1")
                        {
                            newSalary = (0.05F * Salary) + Salary;
                            usuario.Salary = newSalary;
                        }
                        else if (Rol == "2")
                        {
                            newSalary = (0.08F * Salary) + Salary;
                            usuario.Salary = newSalary;
                        }
                        else if (Rol == "3")
                        {
                            newSalary = (0.12F * Salary) + Salary;
                            usuario.Salary = newSalary;
                        }

                        //usuario.IncreasesSalary = usuario.IncreasesSalary + " , " + usuario.Salary;
                        int salarioInt = (int)Math.Round(usuario.Salary);
                        usuario.IncreasesSalary = salarioInt.ToString();
                        _context.Update(usuario);
                        await _context.SaveChangesAsync();
                        message = "SE ACTUALIZO EL SALARIO!";
                    }
                    else { message = "NO SE ACTUALIZÓ EL SALARIO! REVISE LAS FECHAS!!"; }

                }
                else {
                    DateTime dateLastRevision = (DateTime) usuario.LastRevision;
                    dateEnd = dateLastRevision.AddDays(90);
                    auxDate2 = DateTime.Compare(dateCurrent, dateEnd); // menor a cero: 1<2 , cero: 1=2 , mayor a cero: 1>2

                    usuario.IncreasesDates = usuario.IncreasesDates + " , " + dateCurrent.ToShortDateString();
                    
                    if (auxDate2 >=0)
                    {
                        usuario.LastRevision = dateCurrent;
                        String Rol = usuario.Rol;
                        float Salary = usuario.Salary;
                        float newSalary;

                        if (Rol == "1")
                        {
                            newSalary = (0.05F * Salary) + Salary;
                            usuario.Salary = newSalary;
                        }
                        else if (Rol == "2")
                        {
                            newSalary = (0.08F * Salary) + Salary;
                            usuario.Salary = newSalary;
                        }
                        else if (Rol == "3")
                        {
                            newSalary = (0.12F * Salary) + Salary;
                            usuario.Salary = newSalary;
                        }
                        int salarioInt = (int)Math.Round(usuario.Salary);
                        usuario.IncreasesSalary = usuario.IncreasesSalary + " , " + salarioInt.ToString(); 
                        _context.Update(usuario);
                        await _context.SaveChangesAsync();
                        message = "SE ACTUALIZO EL SALARIO!";
                    }
                    else { message = "NO SE ACTUALIZÓ EL SALARIO! REVISE LAS FECHAS!!"; }
                }

            }
            Console.WriteLine("***************************** INFO *****************************");
            Console.WriteLine(message);
            Console.WriteLine("****************************************************************");
            return RedirectToAction(nameof(Index));
        }

        
        public void ExportCSV()
        {
            var CurrentDirectory = Directory.GetCurrentDirectory() + "\\usuarios.csv";
            //TextWriter archivo = new StreamWriter(CurrentDirectory);
            //archivo.Close();

            using (var writer = new StreamWriter(new FileStream(CurrentDirectory, FileMode.Open), System.Text.Encoding.UTF8))
            using (var csvWriter = new CsvHelper.CsvWriter(writer, CultureInfo.CurrentCulture))
            {
                string conexion = "Server=localhost;Database=dbasecrud;User ID=root;Password=root;Pooling=false;";
                MySqlConnection conn = new MySqlConnection(conexion);
                conn.Open();

                //string query = "Select * From contribuyentes Where Cedula = ?pId AND Nacio = ?Nci";
                string query = "Select * From dbasecrud.usuarios";

                MySqlCommand mycommand = new MySqlCommand(query, conn);

                using(MySqlDataReader lector = mycommand.ExecuteReader())
                {
                    if (lector.HasRows)
                    {
                        lector.Read();
                        for (int h = 0; h < lector.FieldCount; h++)
                        {
                            csvWriter.WriteField(lector.GetName(h).ToString());
                        }
                        csvWriter.NextRecord();
                        for (int i=0; i < lector.FieldCount; i++)
                        {
                            csvWriter.WriteField(lector[i].ToString());
                        }
                        csvWriter.NextRecord();
                        while (lector.Read())
                        {
                            for (int x=0; x<lector.FieldCount; x++)
                            {
                                csvWriter.WriteField(lector[x].ToString());
                            }
                            csvWriter.NextRecord();
                        }
                    }

                }

                conn.Close();
                RedirectToAction(nameof(Index));

            }
            

        }

        


    }

   
}
