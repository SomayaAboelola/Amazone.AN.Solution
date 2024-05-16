﻿namespace Amazone.Apis.Helper
{
    public class Pagination<T>

    {
        public Pagination(int pagesize, int pageindex, int count, IReadOnlyList<T> data)
        {
            PageSize = pagesize;
            PageIndex = pageindex;
            Count = count;
            Data = data;
        }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
