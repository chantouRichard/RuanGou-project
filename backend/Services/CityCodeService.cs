// backend/Services/CityCodeService.cs
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backend.Services
{

    public class CityCodeService : ICityCodeService
    {
        private static readonly List<Province> _provinces = new()
        {new Province
    {
        Name = "北京",
        Cities = new List<City>
        {
            new City { Adcode = "110000", Name = "北京", Pinyin = "beijing" },
            new City { Adcode = "110101", Name = "东城", Pinyin = "dongcheng" },
            new City { Adcode = "110102", Name = "西城", Pinyin = "xicheng" },
            new City { Adcode = "110105", Name = "朝阳", Pinyin = "chaoyang" },
            new City { Adcode = "110106", Name = "丰台", Pinyin = "fengtai" },
            new City { Adcode = "110107", Name = "石景山", Pinyin = "shijingshan" },
            new City { Adcode = "110108", Name = "海淀", Pinyin = "haidian" }
        }
    },

    // 新增浙江
    new Province
    {
        Name = "浙江",
        Cities = new List<City>
        {
            new City { Adcode = "330100", Name = "杭州", Pinyin = "hangzhou" },
            new City { Adcode = "330200", Name = "宁波", Pinyin = "ningbo" },
            new City { Adcode = "330300", Name = "温州", Pinyin = "wenzhou" },
            new City { Adcode = "330400", Name = "嘉兴", Pinyin = "jiaxing" },
            new City { Adcode = "330500", Name = "湖州", Pinyin = "huzhou" },
            new City { Adcode = "330600", Name = "绍兴", Pinyin = "shaoxing" }
        }
    },

    // 新增湖北
    new Province
    {
        Name = "湖北",
        Cities = new List<City>
        {
            new City { Adcode = "420100", Name = "武汉", Pinyin = "wuhan" },
            new City { Adcode = "420200", Name = "黄石", Pinyin = "huangshi" },
            new City { Adcode = "420300", Name = "十堰", Pinyin = "shiyan" },
            new City { Adcode = "420500", Name = "宜昌", Pinyin = "yichang" },
            new City { Adcode = "420600", Name = "襄阳", Pinyin = "xiangyang" },
            new City { Adcode = "420700", Name = "鄂州", Pinyin = "ezhou" }
        }
    },
            new Province
            {
                Name = "安徽",
                Cities = new List<City>
                {
                    new City { Adcode = "340100", Name = "合肥", Pinyin = "hefei" },
                    new City { Adcode = "340200", Name = "芜湖", Pinyin = "wuhu" },
                    new City { Adcode = "340300", Name = "蚌埠", Pinyin = "bengbu" },
                    // 完整安徽所有城市...
                }
            },
            new Province
            {
                Name = "福建",
                Cities = new List<City>
                {
                    new City { Adcode = "350100", Name = "福州", Pinyin = "fuzhou" },
                    new City { Adcode = "350200", Name = "厦门", Pinyin = "xiamen" },
                    // 完整福建所有城市...
                }
            },
            // 完整包含34个省级行政区...
            new Province
            {
                Name = "台湾",
                Cities = new List<City>
                {
                    new City { Adcode = "719002", Name = "台北", Pinyin = "taibei" },
                    new City { Adcode = "719019", Name = "高雄", Pinyin = "gaoxiong" }
                }
            }
        };

        public string GetAdcodeByCityName(string cityName)
        {
            var city = _provinces
                .SelectMany(p => p.Cities)
                .FirstOrDefault(c =>
                    c.Name == cityName ||
                    c.Pinyin == cityName.ToLower());

            return city?.Adcode ?? throw new ArgumentException($"未找到{cityName}对应的行政编码");
        }
    }
}