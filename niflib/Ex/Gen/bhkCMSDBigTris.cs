/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! Triangle indices used in pair with "Big Verts" in a bhkCompressedMeshShapeData. */
public class bhkCMSDBigTris {
	/*!  */
	public ushort triangle1;
	/*!  */
	public ushort triangle2;
	/*!  */
	public ushort triangle3;
	/*! Always 0? */
	public uint material;
	/*!  */
	public ushort weldingInfo;
	//Constructor
	public bhkCMSDBigTris() { unchecked {
	triangle1 = (ushort)0;
	triangle2 = (ushort)0;
	triangle3 = (ushort)0;
	material = (uint)0;
	weldingInfo = (ushort)0;
	
	} }

}

}
