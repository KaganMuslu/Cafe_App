using Cafe_App.Models;

namespace Cafe_App.Areas.Admin.Models
{
	public class PersonelViewModel
	{
        public Personel Personel { get; set; }
        public Rol Rol { get; set; }
        public List<Personel> Personeller { get; set; }
        public List<Rol> Roller { get; set; }
    }
}
