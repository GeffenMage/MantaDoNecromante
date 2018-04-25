﻿#pragma checksum "C:\Users\paulo\source\repos\RPG\MantaDoNecromante\MantaNecromante\MainBattle\BattleStage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B364963FD5FEDA18002738BE8E6C662A"
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
                    this.Progress_HP_chosen = (global::Windows.UI.Xaml.Controls.ProgressBar)(target);
                }
                break;
            case 7:
                {
                    this.Progress_MP_chosen = (global::Windows.UI.Xaml.Controls.ProgressBar)(target);
                }
                break;
            case 8:
                {
                    this.Progress_HP_Mob = (global::Windows.UI.Xaml.Controls.ProgressBar)(target);
                }
                break;
            case 9:
                {
                    this.Progress_MP_Mob = (global::Windows.UI.Xaml.Controls.ProgressBar)(target);
                }
                break;
            case 10:
                {
                    this.Start = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 48 "..\..\..\MainBattle\BattleStage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.Start).Click += this.Continue;
                    #line default
                }
                break;
            case 11:
                {
                    this.ExitGame = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 49 "..\..\..\MainBattle\BattleStage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.ExitGame).Click += this.Exit;
                    #line default
                }
                break;
            case 12:
                {
                    this.Skill2 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 41 "..\..\..\MainBattle\BattleStage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.Skill2).Click += this.BotaoSkill;
                    #line default
                }
                break;
            case 13:
                {
                    this.Skill1 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 36 "..\..\..\MainBattle\BattleStage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.Skill1).Click += this.BotaoSkill;
                    #line default
                }
                break;
            case 14:
                {
                    this.Skill0 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 31 "..\..\..\MainBattle\BattleStage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.Skill0).Click += this.BotaoSkill;
                    #line default
                }
                break;
            case 15:
                {
                    this.Start1 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 26 "..\..\..\MainBattle\BattleStage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.Start1).Click += this.BotaoAtacar;
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

