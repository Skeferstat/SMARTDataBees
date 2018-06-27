//	============================================================================
//
//  .,-:::::   :::.    :::::::..   :::::::.      ...   :::.    :::.
//	,;;;'````'   ;;`;;   ;;;;``;;;;   ;;;'';;'  .;;;;;;;.`;;;;,  `;;;
//	[[[         ,[[ '[[,  [[[,/[[['   [[[__[[\.,[[     \[[,[[[[[. '[[
//	$$$        c$$$cc$$$c $$$$$$c     $$""""Y$$$$$,     $$$$$$ "Y$c$$
//	`88bo,__,o, 888   888,888b "88bo,_88o,,od8P"888,_ _,88P888    Y88
//	"YUMMMMMP"YMM   ""` MMMM   "W" ""YUMMMP"   "YMMMMMP" MMM     YM
//
//	============================================================================
//
//	This file is a part of the Carbon Framework.
//
//	Copyright (C) 2005 Mark (Code6) Belles 
//
//	This library is free software; you can redistribute it and/or
//	modify it under the terms of the GNU Lesser General Public
//	License as published by the Free Software Foundation; either
//	version 2.1 of the License, or (at your option) any later version.
//
//	This library is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//	Lesser General Public License for more details.
//
//	You should have received a copy of the GNU Lesser General Public
//	License along with this library; if not, write to the Free Software
//	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//
//	============================================================================

namespace Carbon.AutoUpdate.Common
{
	/// <summary>
	/// Provides information relating to a downloadable .Update for a specific product
	/// </summary>
	public sealed class AutoUpdateDownloadDescriptor
	{
		private AutoUpdateManifest _manifest;
		private AutoUpdateDownloader _downloader;
		private AutoUpdateOptions _options;
		private string _downloadedPath;

		/// <summary>
		/// Initializes a new instance of the AutoUpdateDownloadDescriptor class
		/// </summary>
		public AutoUpdateDownloadDescriptor()
		{
			_manifest = new AutoUpdateManifest();
			_downloader = null;
			_options = null;
		}

		/// <summary>
		/// Initializes a new instance of the AutoUpdateDownloadDescriptor class
		/// </summary>
		/// <param name="manifest">A manifest file containing information about the product, and a summary of the changes new to the version specified</param>
		/// <param name="downloader">The downloader that will be responsible for downloading the .update</param>
		/// <param name="options">The options to be used by the downloader while downloading the .update file</param>
		public AutoUpdateDownloadDescriptor(AutoUpdateManifest manifest, AutoUpdateDownloader downloader, AutoUpdateOptions options)
		{
			_manifest = manifest;
			_downloader = downloader;
			_options = options;						
		}

		#region My Public Properties

		/// <summary>
		/// Gets or sets the manifest that describes the update for the product it goes to, and the changes contained in it
		/// </summary>
		public AutoUpdateManifest Manifest
		{
			get
			{
				return _manifest;
			}
			set
			{
				_manifest = value;
			}
		}

		/// <summary>
		/// Gets or sets the manager that will be used to manage the update described by this descriptor
		/// </summary>
		public AutoUpdateDownloader Downloader
		{
			get
			{
				return _downloader;
			}
			set
			{
				_downloader = value;    
			}
		}
		
		/// <summary>
		/// Gets or sets the options the be used to control the behavior of the auto update
		/// </summary>
		public AutoUpdateOptions Options
		{
			get
			{
				return _options;
			}
			set
			{
				_options = value;
			}
		}		
		
		/// <summary>
		/// Gets or sets the full path including filename where the .update file was downloaded
		/// </summary>
		public string DownloadedPath
		{
			get
			{
				return _downloadedPath;
			}
			set
			{
				_downloadedPath = value;
			}
		}

		#endregion

		#region My Public Static Methods

		/// <summary>
		/// Bubble sorts the elements in the descriptor array using their product verion (The newest version will be at element 0).
		/// </summary>
		/// <param name="updates"></param>
		/// <returns></returns>
		public static AutoUpdateDownloadDescriptor[] Sort(AutoUpdateDownloadDescriptor[] updates)
		{
			// front to back - 1 
			for(var i = 0; i < updates.Length - 1; i++)
			{
				// front + 1 to back
				for(var j = i + 1; j < updates.Length; j++)
				{			
					if (updates[i].Manifest.Product.Version < updates[j].Manifest.Product.Version)
					{											 
						// swap i with j, where i=1 and j=2
						var update = updates[j];
						updates[j] = updates[i];
						updates[i] = update;
					}													
				}
			}
			return updates;
		}

		#endregion
	}
}
