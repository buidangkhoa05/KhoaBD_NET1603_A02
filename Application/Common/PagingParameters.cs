﻿namespace Team5.Domain.Common
{
    public class PagingParameters
    {
        const int maxPageSize = 50;
        private int _pageSize = 0;

        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public PagingParameters()
        {
            PageNumber = 1;
            PageSize = 3;
        }

        public PagingParameters(int? pageNumer, int? pageSize)
        {
            PageNumber = pageNumer ?? 1;
            PageSize = pageSize ?? 3;
        }

    }
}
