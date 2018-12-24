/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class bhkPositionConstraintMotor {
	/*! Minimum motor force */
	public float minForce;
	/*! Maximum motor force */
	public float maxForce;
	/*! Relative stiffness */
	public float tau;
	/*! Motor damping value */
	public float damping;
	/*! A factor of the current error to calculate the recovery velocity */
	public float proportionalRecoveryVelocity;
	/*! A constant velocity which is used to recover from errors */
	public float constantRecoveryVelocity;
	/*! Is Motor enabled */
	public bool motorEnabled;
	//Constructor
	public bhkPositionConstraintMotor() { unchecked {
	minForce = -1000000.0f;
	maxForce = 1000000.0f;
	tau = 0.8f;
	damping = 1.0f;
	proportionalRecoveryVelocity = 2.0f;
	constantRecoveryVelocity = 1.0f;
	motorEnabled = 0;
	
	} }

}

}