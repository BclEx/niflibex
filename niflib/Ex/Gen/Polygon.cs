/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! Two dimensional screen elements. */
public class Polygon {
	/*!  */
	public ushort numVertices;
	/*! Offset in vertex array. */
	public ushort vertexOffset;
	/*!  */
	public ushort numTriangles;
	/*! Offset in indices array. */
	public ushort triangleOffset;
	//Constructor
	public Polygon() { unchecked {
	numVertices = (ushort)0;
	vertexOffset = (ushort)0;
	numTriangles = (ushort)0;
	triangleOffset = (ushort)0;
	
	} }

}

}
