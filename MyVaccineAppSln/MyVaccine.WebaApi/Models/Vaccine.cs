namespace MyVaccine.WebaApi.Models;

public class Vaccine : BaseTable
{
    public int VaccineId { get; set; }
    public string Name { set; get; }
    public List<VaccineCategory> Categories { get; set; }
    public bool RequiresBooster { get; set; }
}
