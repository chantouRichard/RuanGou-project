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

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        private static extern void Everything_SetSearch(string lpSearchString);

        [DllImport(DllName)]
        private static extern bool Everything_Query(bool bWait);

        [DllImport(DllName)]
        private static extern int Everything_GetNumResults();

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        private static extern void Everything_GetResultFullPathName(int nIndex, StringBuilder lpString, int nMaxCount);

        [DllImport(DllName)]
        private static extern uint Everything_GetLastError();

        public static Task<List<string>> SearchAsync(string keyword)
        {
            return Task.Run(() =>
            {
                Everything_SetSearch(keyword);
                bool querySuccess = Everything_Query(true);

                if (!querySuccess)
                {
                    uint errorCode = Everything_GetLastError();
                    throw new Exception($"Everything 查询失败，错误码: {errorCode}");
                }

                int count = Everything_GetNumResults();
                var results = new List<string>(count);

                for (int i = 0; i < count && i < 100; i++)
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
