using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace frontend.Services
{
    public static class EverythingWrapper
    {
        private const string DllName = "Everything64.dll";


        //设置搜索关键词
        [DllImport(DllName, CharSet = CharSet.Unicode)]
        private static extern void Everything_SetSearch(string lpSearchString);


        //执行搜索操作，bWait==true表示等待搜索完成
        [DllImport(DllName)]
        private static extern bool Everything_Query(bool bWait);

        [DllImport(DllName)]
        //获取搜索结果数量
        private static extern int Everything_GetNumResults();

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        //获取指定索引的文件路径
        private static extern void Everything_GetResultFullPathName(int nIndex, StringBuilder lpString, int nMaxCount);


        [DllImport(DllName)]
        //获取最近一次错误的错误码
        private static extern uint Everything_GetLastError();

        //核心方法，用于异步搜索关键词相关的文件路径
        public static Task<List<string>> SearchAsync(string keyword)
        {
            return Task.Run(() =>
            {
                //关键词+执行搜索
                Everything_SetSearch(keyword);
                bool querySuccess = Everything_Query(true);

                //查询失败
                if (!querySuccess)
                {
                    uint errorCode = Everything_GetLastError();
                    throw new Exception($"Everything 查询失败，错误码: {errorCode}");
                }

                //获取结果
                int count = Everything_GetNumResults();
                var results = new List<string>(count);

                for (int i = 0; i < count && i < 100; i++)  //最多返回100条结果
                {
                    StringBuilder sb = new StringBuilder(260);
                    Everything_GetResultFullPathName(i, sb, sb.Capacity);
                    results.Add(sb.ToString());
                }

                return results;
            });
        }
    }
}
