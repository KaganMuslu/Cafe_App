using Cafe_App.Models;

namespace Cafe_App.Areas.Admin.Models
{
	public class RolViewModel
	{
        public Rol Rol { get; set; }

        public List<Rol> Roller { get; set; }

        public List<AnonimTip> Personeller { get; set; }
    }

	public class AnonimTip
	{
		public int RolId { get; set; }
		public string RolAdi { get; set; }
		public List<Personel> PersonelListesi { get; set; }
	}
}
