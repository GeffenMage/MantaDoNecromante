﻿#pragma checksum "C:\Users\Daniel Sousa Duplat\source\repos\MantaDoNecromante2\MantaNecromante\MainBattle\BattleStage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FE72F1484AE5C1BD5F064100A78FBFF0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MantaNecromante.MainBattle
{
    partial class BattleStage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.Floor = (global::Windows.UI.Xaml.Controls.Canvas)(target);
                }
                break;
            case 2:
                {
                    this.Mansion = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 3:
                {
                    this.Hero = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 4:
                {
                    this.Foe = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 5:
                {
                    this.OptionsMenu = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 6:
                {
                    this.Start = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 19 "..\..\..\MainBattle\BattleStage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.Start).Click += this.Continue;
                    #line default
                }
                break;
            case 7:
                {
                    this.ExitGame = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 20 "..\..\..\MainBattle\BattleStage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.ExitGame).Click += this.Exit;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

