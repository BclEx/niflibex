/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class NxJointDriveDesc {
	/*!  */
	public NxD6JointDriveType driveType;
	/*!  */
	public float restitution;
	/*!  */
	public float spring;
	/*!  */
	public float damping;
	//Constructor
	public NxJointDriveDesc() { unchecked {
	driveType = (NxD6JointDriveType)0;
	restitution = 0.0f;
	spring = 0.0f;
	damping = 0.0f;
	
	} }

}

}
