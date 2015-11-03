﻿namespace Aritter.Domain.Contracts
{
    public interface IEntity
    {
        #region Properties

        int Id { get; set; }

        bool Enabled { get; set; }

        #endregion Properties

        #region Methods

        void Enable();
        void Disable();

        #endregion
    }
}