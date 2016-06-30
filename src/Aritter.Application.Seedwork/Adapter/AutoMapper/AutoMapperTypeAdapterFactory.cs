﻿using Aritter.Infra.Crosscutting.Adapter;

namespace Aritter.Application.Seedwork.Adapter.AutoMapper
{
    public class AutoMapperTypeAdapterFactory : ITypeAdapterFactory
    {
        #region Members

        private ITypeAdapter currentAdapter;

        #endregion

        #region ITypeAdapterFactory Members

        public ITypeAdapter Create()
        {
            if (currentAdapter == null)
            {
                currentAdapter = new AutoMapperTypeAdapter();
            }

            return currentAdapter;
        }

        #endregion
    }

}