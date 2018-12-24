/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! Bethesda Animation. Describes a furniture position? */
public class FurniturePosition {
	/*! Offset of furniture marker. */
	public Vector3 offset;
	/*! Furniture marker orientation. */
	public ushort orientation;
	/*!
	 * Refers to a furnituremarkerxx.nif file. Always seems to be the same as Position
	 * Ref 2.
	 */
	public byte positionRef1;
	/*!
	 * Refers to a furnituremarkerxx.nif file. Always seems to be the same as Position
	 * Ref 1.
	 */
	public byte positionRef2;
	/*! Similar to Orientation, in float form. */
	public float heading;
	/*! Unknown */
	public AnimationType animationType;
	/*! Unknown/unused in nif? */
	public FurnitureEntryPoints entryProperties;
	//Constructor
	public FurniturePosition() { unchecked {
	orientation = (ushort)0;
	positionRef1 = (byte)0;
	positionRef2 = (byte)0;
	heading = 0.0f;
	animationType = (AnimationType)0;
	entryProperties = (FurnitureEntryPoints)0;
	
	} }

}

}
