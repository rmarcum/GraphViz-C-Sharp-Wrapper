﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRegisterLayoutPluginCommand.cs" company="Jamie Dixon Ltd">
//   Jamie Dixon
// </copyright>
// <summary>
//   Defines the IRegisterLayoutPluginCommand type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GraphVizWrapper.Commands
{
    public interface IRegisterLayoutPluginCommand
    {
        void Invoke(string configFilePath, Enums.RenderingEngine renderingEngine); 
    }
}
