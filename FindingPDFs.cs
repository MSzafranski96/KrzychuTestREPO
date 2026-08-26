using System.ComponentModel;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;
IEnumerable<string> RegistrationList = ["4K-8888"
                                       ,"4K-AZ03"
                                       ,"4K-AZ04"
                                       ,"4K-AZ05"
                                       ,"4K-AZ77"
                                       ,"4K-AZ78"
                                       ,"4K-AZ79"
                                       ,"4K-AZ80"
                                       ,"4K-AI07"
                                       ,"4K-AZ83"
                                       ,"4K-AZ84"
                                       ,"4K-AZ141"
                                       ,"4K-AZ142"
                                       ,"4K-AZ143"
                                       ,"VP-BAK"
                                       ,"VP-BBK"
                                       ,"4K-AI08"
                                       ,"4K-AZ85"
                                       ,"4K-AZ86"
                                       ,"4K-AZ11"
                                       ,"4K-AZ12"
                                       ,"4K-AI01"
                                       ,"4K-AZ81"
                                       ,"4K-AZ82"
                                       ,"4K-AI001"
                                       ,"VP-BBR"
                                       ,"VP-BBS"
                                       ,"4K-AZ140"
                                       ,"VP-BHE"
                                       ,"VP-BHH"
                                       ,"VP-BRU"
                                       ,"VP-BRV"
                                       ,"4K-AZ65"
                                       ,"4K-AZ64"
                                       ,"4K-AZ66"
                                       ,"4K-AZ67"];

IEnumerable<string> FileNamesDamage = ["DAMAGE", "DBC"];
IEnumerable<string> FileNamesRepair = ["REPAIR", "RMRS"];
List<RegistrationFile> RegistrationFilesList = [];
List<RegistrationFile> Allpaths = [];

foreach (var path in Directory.GetFiles("C:\\Users\\michals\\Downloads\\Structural Damages and Repairs\\Structural Damages and Repairs", "*.pdf", SearchOption.AllDirectories))
{
    var parts = path.Split(Path.DirectorySeparatorChar);
    string Model = parts[^3];
    string Reg = parts[^2];
    var FileName = parts.Last();
    Allpaths.Add(new RegistrationFile { model = Model, reg = Reg, fileName = FileName,  filePath = path });
}

var AllpathsFilter = Allpaths.Where(regfile => FileNamesDamage.Any(dmg => regfile.fileName.Contains(dmg, StringComparison.OrdinalIgnoreCase)) || FileNamesRepair.Any(rep => regfile.fileName.Contains(rep, StringComparison.OrdinalIgnoreCase)));
//foreach (var regfile in AllpathsFilter.Where(regfile => !RegistrationList.Any(reg => regfile.reg.Contains(reg, StringComparison.OrdinalIgnoreCase))))
//{
//    Console.WriteLine("File is in an incorrect folder for Path: {0} ", regfile.filePath);
//}
//;
AllpathsFilter = AllpathsFilter.Where(regfile => RegistrationList.Any(reg => regfile.reg.Contains(reg, StringComparison.OrdinalIgnoreCase)));
var AllpathsGroupedBy = AllpathsFilter.GroupBy(k => new { k.model, k.reg });

foreach (var group in AllpathsGroupedBy)
{
    var NumberOfDamageFiles = 0;
    var NumberOfRepairFiles = 0;
        foreach (var regfile in group)
        {
            RegistrationFilesList.Add(regfile);
            if (FileNamesDamage.Any(dmg => regfile.fileName.Contains(dmg, StringComparison.OrdinalIgnoreCase))) { NumberOfDamageFiles++; }
            else if (FileNamesRepair.Any(rep => regfile.fileName.Contains(rep, StringComparison.OrdinalIgnoreCase))) { NumberOfRepairFiles++; }
            ;
        }
    if (group.Count() != 2) { Console.WriteLine(value: $"{NumberOfDamageFiles} damage file(s) and {NumberOfRepairFiles} repair file(s) found for Model: {group.Key.model} Reg: {group.Key.reg} ({NumberOfDamageFiles + NumberOfRepairFiles} total)-> should be 1 of each file"); }
    else if (NumberOfDamageFiles == 0) { Console.WriteLine(value: $"Missing Damages file for Model: {group.Key.model} Reg: {group.Key.reg}"); }
    else if (NumberOfRepairFiles == 0) { Console.WriteLine(value: $"Missing Repairs file for Model: {group.Key.model} Reg: {group.Key.reg}"); }

    ;
}
public struct RegistrationFile
{
    public required string model { get; set; }
    public required string reg { get; set; }
    public required string fileName { get; set; }
    public required string filePath { get; set; }

};