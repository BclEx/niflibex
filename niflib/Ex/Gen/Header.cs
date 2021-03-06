/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! The NIF file header. */
public class Header {
	/*!
	 * 'NetImmerse File Format x.x.x.x' (versions <= 10.0.1.2) or 'Gamebryo File Format
	 * x.x.x.x' (versions >= 10.1.0.0), with x.x.x.x the version written out. Ends with
	 * a newline character (0x0A).
	 */
	internal HeaderString headerString;
	/*!  */
	internal Array3<LineString> copyright;
	/*!
	 * The NIF version, in hexadecimal notation: 0x04000002, 0x0401000C, 0x04020002,
	 * 0x04020100, 0x04020200, 0x0A000100, 0x0A010000, 0x0A020000, 0x14000004, ...
	 */
	internal uint version;
	/*! Determines the endianness of the data in the file. */
	internal EndianType endianType;
	/*! An extra version number, for companies that decide to modify the file format. */
	internal uint userVersion;
	/*! Number of file objects. */
	internal uint numBlocks;
	/*!  */
	internal uint userVersion2;
	/*!  */
	internal ExportInfo exportInfo;
	/*!  */
	internal ShortString maxFilepath;
	/*!  */
	internal ByteArray metadata;
	/*! Number of object types in this NIF file. */
	internal ushort numBlockTypes;
	/*! List of all object types used in this NIF file. */
	internal IList<string> blockTypes;
	/*!
	 * Maps file objects on their corresponding type: first file object is of type
	 * object_types[object_type_index[0]], the second of
	 * object_types[object_type_index[1]], etc.
	 */
	internal IList<ushort> blockTypeIndex;
	/*! Array of block sizes? */
	internal IList<uint> blockSize;
	/*! Number of strings. */
	internal uint numStrings;
	/*! Maximum string length. */
	internal uint maxStringLength;
	/*! Strings. */
	internal IList<string> strings;
	/*!  */
	internal uint numGroups;
	/*!  */
	internal IList<uint> groups;
	//Constructor
	public Header() { unchecked {
	version = (uint)0x04000002;
	endianType = EndianType.ENDIAN_LITTLE;
	userVersion = (uint)0;
	numBlocks = (uint)0;
	userVersion2 = (uint)0;
	numBlockTypes = (ushort)0;
	numStrings = (uint)0;
	maxStringLength = (uint)0;
	numGroups = (uint)0;
	
	} }

	public NifInfo Read(IStream s) {
		//Declare NifInfo structure
		var info = new NifInfo();

		Nif.NifStream(out headerString, s, info);
		if (info.version <= 0x03010000) {
			for (var i3 = 0; i3 < 3; i3++) {
				Nif.NifStream(out copyright[i3], s, info);
			}
		}
		if (info.version >= 0x03010001) {
			Nif.NifStream(out version, s, info);
		}
		if (info.version >= 0x14000003) {
			Nif.NifStream(out endianType, s, info);
		}
		if (info.version >= 0x0A000108) {
			Nif.NifStream(out userVersion, s, info);
		}
		if (info.version >= 0x03010001) {
			Nif.NifStream(out numBlocks, s, info);
		}
		if (((version == 0x0A000102) || (((version == 0x14020007) || ((version == 0x14000005) || ((version >= 0x0A010000) && ((version <= 0x14000004) && (userVersion <= 11))))) && (userVersion >= 3)))) {
			Nif.NifStream(out userVersion2, s, info);
			Nif.NifStream(out exportInfo.author, s, info);
			Nif.NifStream(out exportInfo.processScript, s, info);
			Nif.NifStream(out exportInfo.exportScript, s, info);
		}
		if ((userVersion2 == 130)) {
			Nif.NifStream(out maxFilepath, s, info);
		}
		if (info.version >= 0x1E000000) {
			Nif.NifStream(out metadata.dataSize, s, info);
			metadata.data = new byte[metadata.dataSize];
			for (var i3 = 0; i3 < metadata.data.Count; i3++) {
				Nif.NifStream(out metadata.data[i3], s, info);
			}
		}
		if (info.version >= 0x05000001) {
			Nif.NifStream(out numBlockTypes, s, info);
			blockTypes = new string[numBlockTypes];
			for (var i3 = 0; i3 < blockTypes.Count; i3++) {
				Nif.NifStream(out blockTypes[i3], s, info);
			}
			blockTypeIndex = new ushort[numBlocks];
			for (var i3 = 0; i3 < blockTypeIndex.Count; i3++) {
				Nif.NifStream(out blockTypeIndex[i3], s, info);
			}
		}
		if (info.version >= 0x14020005) {
			blockSize = new uint[numBlocks];
			for (var i3 = 0; i3 < blockSize.Count; i3++) {
				Nif.NifStream(out blockSize[i3], s, info);
			}
		}
		if (info.version >= 0x14010001) {
			Nif.NifStream(out numStrings, s, info);
			Nif.NifStream(out maxStringLength, s, info);
			strings = new string[numStrings];
			for (var i3 = 0; i3 < strings.Count; i3++) {
				Nif.NifStream(out strings[i3], s, info);
			}
		}
		if (info.version >= 0x05000006) {
			Nif.NifStream(out numGroups, s, info);
			groups = new uint[numGroups];
			for (var i3 = 0; i3 < groups.Count; i3++) {
				Nif.NifStream(out groups[i3], s, info);
			}
		}

		//Copy info.version to local version var.
		version = info.version;

		//Fill out and return NifInfo structure.
		info.userVersion = userVersion;
		info.userVersion2 = userVersion2;
		info.endian = (EndianType)endianType;
		info.author = exportInfo.author.str;
		info.processScript = exportInfo.processScript.str;
		info.exportScript = exportInfo.exportScript.str;
		return info;
	}

	public void Write(OStream s, NifInfo info) {
		numGroups = (uint)groups.Count;
		numStrings = (uint)strings.Count;
		numBlockTypes = (ushort)blockTypes.Count;
		numBlocks = (uint)blockTypeIndex.Count;
		Nif.NifStream(headerString, s, info);
		if (info.version <= 0x03010000) {
			for (var i3 = 0; i3 < 3; i3++) {
				Nif.NifStream(copyright[i3], s, info);
			}
		}
		if (info.version >= 0x03010001) {
			Nif.NifStream(version, s, info);
		}
		if (info.version >= 0x14000003) {
			Nif.NifStream(endianType, s, info);
		}
		if (info.version >= 0x0A000108) {
			Nif.NifStream(userVersion, s, info);
		}
		if (info.version >= 0x03010001) {
			Nif.NifStream(numBlocks, s, info);
		}
		if (((version == 0x0A000102) || (((version == 0x14020007) || ((version == 0x14000005) || ((version >= 0x0A010000) && ((version <= 0x14000004) && (userVersion <= 11))))) && (userVersion >= 3)))) {
			Nif.NifStream(userVersion2, s, info);
			Nif.NifStream(exportInfo.author, s, info);
			Nif.NifStream(exportInfo.processScript, s, info);
			Nif.NifStream(exportInfo.exportScript, s, info);
		}
		if ((userVersion2 == 130)) {
			Nif.NifStream(maxFilepath, s, info);
		}
		if (info.version >= 0x1E000000) {
			metadata.dataSize = (uint)metadata.data.Count;
			Nif.NifStream(metadata.dataSize, s, info);
			for (var i3 = 0; i3 < metadata.data.Count; i3++) {
				Nif.NifStream(metadata.data[i3], s, info);
			}
		}
		if (info.version >= 0x05000001) {
			Nif.NifStream(numBlockTypes, s, info);
			for (var i3 = 0; i3 < blockTypes.Count; i3++) {
				Nif.NifStream(blockTypes[i3], s, info);
			}
			for (var i3 = 0; i3 < blockTypeIndex.Count; i3++) {
				Nif.NifStream(blockTypeIndex[i3], s, info);
			}
		}
		if (info.version >= 0x14020005) {
			for (var i3 = 0; i3 < blockSize.Count; i3++) {
				Nif.NifStream(blockSize[i3], s, info);
			}
		}
		if (info.version >= 0x14010001) {
			Nif.NifStream(numStrings, s, info);
			Nif.NifStream(maxStringLength, s, info);
			for (var i3 = 0; i3 < strings.Count; i3++) {
				Nif.NifStream(strings[i3], s, info);
			}
		}
		if (info.version >= 0x05000006) {
			Nif.NifStream(numGroups, s, info);
			for (var i3 = 0; i3 < groups.Count; i3++) {
				Nif.NifStream(groups[i3], s, info);
			}
		}
	}

	public string AsString(bool verbose = false) {
		var s = new System.Text.StringBuilder();
		uint array_output_count = 0;
		numGroups = (uint)groups.Count;
		numStrings = (uint)strings.Count;
		numBlockTypes = (ushort)blockTypes.Count;
		numBlocks = (uint)blockTypeIndex.Count;
		s.AppendLine($"    Header String:  {headerString}");
		array_output_count = 0;
		for (var i2 = 0; i2 < 3; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Copyright[{i2}]:  {copyright[i2]}");
			array_output_count++;
		}
		s.AppendLine($"    Version:  {version}");
		s.AppendLine($"    Endian Type:  {endianType}");
		s.AppendLine($"    User Version:  {userVersion}");
		s.AppendLine($"    Num Blocks:  {numBlocks}");
		if (((version == 0x0A000102) || (((version == 0x14020007) || ((version == 0x14000005) || ((version >= 0x0A010000) && ((version <= 0x14000004) && (userVersion <= 11))))) && (userVersion >= 3)))) {
			s.AppendLine($"      User Version 2:  {userVersion2}");
			s.AppendLine($"      Author:  {exportInfo.author}");
			s.AppendLine($"      Process Script:  {exportInfo.processScript}");
			s.AppendLine($"      Export Script:  {exportInfo.exportScript}");
		}
		if ((userVersion2 == 130)) {
			s.AppendLine($"      Max Filepath:  {maxFilepath}");
		}
		metadata.dataSize = (uint)metadata.data.Count;
		s.AppendLine($"    Data Size:  {metadata.dataSize}");
		array_output_count = 0;
		for (var i2 = 0; i2 < metadata.data.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Data[{i2}]:  {metadata.data[i2]}");
			array_output_count++;
		}
		s.AppendLine($"    Num Block Types:  {numBlockTypes}");
		array_output_count = 0;
		for (var i2 = 0; i2 < blockTypes.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Block Types[{i2}]:  {blockTypes[i2]}");
			array_output_count++;
		}
		array_output_count = 0;
		for (var i2 = 0; i2 < blockTypeIndex.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Block Type Index[{i2}]:  {blockTypeIndex[i2]}");
			array_output_count++;
		}
		array_output_count = 0;
		for (var i2 = 0; i2 < blockSize.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Block Size[{i2}]:  {blockSize[i2]}");
			array_output_count++;
		}
		s.AppendLine($"    Num Strings:  {numStrings}");
		s.AppendLine($"    Max String Length:  {maxStringLength}");
		array_output_count = 0;
		for (var i2 = 0; i2 < strings.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Strings[{i2}]:  {strings[i2]}");
			array_output_count++;
		}
		s.AppendLine($"    Num Groups:  {numGroups}");
		array_output_count = 0;
		for (var i2 = 0; i2 < groups.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Groups[{i2}]:  {groups[i2]}");
			array_output_count++;
		}
		return s.ToString();
	}
	//--BEGIN:MISC--//
        /*! HeaderString
         * \return The header string
         */
        public HeaderString HeaderString => headerString;

        /*! Nif Version
         * \return The current nif version
         */
        public uint Version => version;

        /*! Endian type
         * \return The endian type. Intel machines use small endian
         */
        public EndianType EndianType => endianType;

        /*! User version
         * \return The user version of this file which refers to the company/game that used the netimmerse/gamebryo engine
         */
        public uint UserVersion => userVersion;

        /*! User version2
         * \return The user version of this file which refers to the company/game that used the netimmerse/gamebryo engine
         */
        public uint UserVersion2 => userVersion2;

        /*! Block type
         * \return A vector containing all the names of the block types in the nif file. Useful for determing what does the nif contain
         */
        public string[] BlockTypes => blockTypes;

        /*! Block type
         * \return A vector containing all the indexes in the block types list that denote the type of each block in the nif
         */
        public ushort[] BlockTypeIndex => blockTypeIndex;
	//--END:CUSTOM--//
}

}
