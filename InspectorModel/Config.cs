using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectorModel
{
    /// <summary>
    /// コンフィグ
    /// </summary>
    [Serializable]
    public class Config
    {
        public static Config Instance { get; set; } = new Config();

        /// <summary>
        /// ルートディレクトリ
        /// </summary>
        public string Root;
    }
}
