﻿using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using WorldLoader.Attributes;
using WorldLoader.HookUtils;
using WorldLoader.ModulesLibs.UserInterface;
using WorldLoader.Utils;

namespace WorldLoader.Mods
{
	public class UnityMod : WLMod
	{
		public UnityMod() {
		}

		public UnityMod(string name, ConsoleColor color = ConsoleColor.DarkRed) {
			Name = name;
			ModColor = color;
		}

		internal UnityMod(Type type) {
			this.type = type;
		}

		public ModManager ModManager { get; private set; }
		public UnityMod Mod { get; private set; }
		public bool AllowUnloading { get; set; } = true;


		public virtual void OnUnload() { 
			
		}

		public void Unload() {
			OnUnload();
			ModManager.UnloadMod(this);
		}


		internal void Initialize(ModAttribute ModInfo, ModManager ModMger)
		{
			Name = ModInfo.Name;
			Version = ModInfo.Version;
			Author = ModInfo.Author;
			ModManager = ModMger;
			Mod = this;
			harmonyInstance = new(Name + Version.Random());
		}

		/// <summary>
		///  Runs OnInject
		/// </summary>
		public virtual void OnInject()
		{
		}

		/// <summary>
		///  Runs a few times EveryFrame
		/// </summary>
		public virtual void OnGui()
		{
		}

	}
}