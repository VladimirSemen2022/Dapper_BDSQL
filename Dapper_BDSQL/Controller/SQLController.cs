using System.IO;
using System.Text.Json;
using Work_with_SQL_Table__HW_4_;

namespace Dapper_BDSQL.Controller
{
    class SQLController
    {
        readonly string fileName;
        public DBSettings defaultsetting { get; set; }
        public SQLController(string fileName = "setting.json")
        {
            this.fileName = fileName;
        }
        //Загрузка данных о параметрах подключения к БД из файла в формате JSON и их хранение в члене класса формата DBSettings
        public void Download() => defaultsetting = JsonSerializer.Deserialize<DBSettings>(File.ReadAllText(fileName));
        public void SetSettings(DBSettings setting)
        {
            defaultsetting = setting;
        }
        //Сохранение параметров подключения к БД в файле в формате JSON
        public void Save() => File.WriteAllText(fileName, JsonSerializer.Serialize<DBSettings>(defaultsetting));
    }
}
