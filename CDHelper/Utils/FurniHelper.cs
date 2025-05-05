using System.Reflection;
using CDHelper.Models.PacketData;
using CDHelper.Structs;
using Xabbo.Core;

namespace CDHelper.Utils
{
    public class FurniHelper
    {
        /// <summary>
        /// Extracts CD information from a furni array, using both embedded data and catalog metadata if available.
        /// Extrai informacoes dos CDs de um array de mobis, usando dados embutidos e dados do catalogo se disponiveis.
        /// </summary>
        public List<CdData> GetCdData(IItem[] furniArray)
        {
            var list = new List<CdData>();

            foreach (var furni in furniArray)
            {
                // Gets raw embedded data from the furni
                // Pega os dados brutos embutidos do mobi
                string? dataValue = GetDataValue(furni);

                // Gets the extra value (song ID)
                // Pega o valor extra (ID da musica)
                int? extraValue = GetExtraValue(furni);

                // Validates if dataValue and extraValue are valid
                // Valida se os dados sao validos
                if (!string.IsNullOrEmpty(dataValue) && extraValue is not null)
                {
                    var parts = dataValue.Split('\n');
                    if (parts.Length >= 6)
                    {
                        string title = parts[5];
                        string author = parts[0];

                        // Tries to get catalog metadata by song ID
                        // Tenta obter metadados do catalogo pelo ID da musica
                        var catalogData = CatalogMusicData.Data.FirstOrDefault(x => x.Id == extraValue);

                        if (catalogData != null)
                        {
                            // If available, uses catalog data
                            // Se disponivel, usa os dados do catalogo
                            author = catalogData.Author;
                            title = catalogData.Title;
                        }

                        var cd = new CdData(title, author);
                        list.Add(cd);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Retrieves the extra value (song ID) from a given furni item using reflection.
        /// Recupera o valor extra (ID da musica) de um item furni usando reflexao.
        /// </summary>
        public int? GetExtraValue(IItem furni)
        {
            // Uses reflection to access the 'Extra' property of the item
            // Usa reflexao para acessar a propriedade 'Extra' do item
            var extraProp = furni.GetType().GetProperty("Extra", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            // Gets the raw value of the Extra property
            // Obtem o valor bruto da propriedade Extra
            var rawExtra = extraProp?.GetValue(furni);

            if (rawExtra is long longValue && longValue >= int.MinValue && longValue <= int.MaxValue)
            {
                return (int)longValue;
            }

            return null;
        }

        /// <summary>
        /// Retrieves the string data value from a furni item using reflection.
        /// Recupera o valor de dados (string) de um item furni usando reflexao.
        /// </summary>
        public string? GetDataValue(IItem furni)
        {
            // Gets the 'Data' property using reflection
            // Pega a propriedade 'Data' usando reflexao
            var dataProp = furni.GetType().GetProperty("Data", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            var data = dataProp?.GetValue(furni);

            if (data != null)
            {
                // Gets the 'Value' property from the 'Data' object
                // Pega a propriedade 'Value' do objeto 'Data'
                var valueProp = data.GetType().GetProperty("Value", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                return valueProp?.GetValue(data) as string;
            }

            return null;
        }

    }
}
