/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//-----------------------------------NOTICE----------------------------------//
// Some of this file is automatically filled in by a Python script.  Only    //
// add custom code in the designated areas or it will be overwritten during  //
// the next update.                                                          //
//-----------------------------------NOTICE----------------------------------//

using System;
using System.IO;
using System.Collections.Generic;


namespace Niflib {

/*!
 * This is the default body type for all "normal" usable and static world objects.
 * The "T" suffix
 *         marks this body as active for translation and rotation, a normal
 * bhkRigidBody ignores those
 *         properties. Because the properties are equal, a bhkRigidBody may be
 * renamed into a bhkRigidBodyT and vice-versa.
 */
public class bhkRigidBody : bhkEntity {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkRigidBody", bhkEntity.TYPE);
	/*!
	 * How the body reacts to collisions. See hkResponseType for hkpWorld default
	 * implementations.
	 */
	internal hkResponseType collisionResponse;
	/*! Skipped over when writing Collision Response and Callback Delay. */
	internal byte unusedByte1;
	/*!
	 * Lowers the frequency for processContactCallbacks. A value of 5 means that a
	 * callback is raised every 5th frame. The default is once every 65535 frames.
	 */
	internal ushort processContactCallbackDelay;
	/*! Unknown. */
	internal uint unknownInt1;
	/*! Copy of Havok Filter */
	internal HavokFilter havokFilterCopy;
	/*! Garbage data from memory. Matches previous Unused value. */
	internal Array4<byte> unused2;
	/*! Unknown. */
	internal uint unknownInt2;
	/*!  */
	internal hkResponseType collisionResponse2;
	/*! Skipped over when writing Collision Response and Callback Delay. */
	internal byte unusedByte2;
	/*!  */
	internal ushort processContactCallbackDelay2;
	/*!
	 * A vector that moves the body by the specified amount. Only enabled in
	 * bhkRigidBodyT objects.
	 */
	internal Vector4 translation;
	/*!
	 * The rotation Yaw/Pitch/Roll to apply to the body. Only enabled in bhkRigidBodyT
	 * objects.
	 */
	internal hkQuaternion rotation;
	/*! Linear velocity. */
	internal Vector4 linearVelocity;
	/*! Angular velocity. */
	internal Vector4 angularVelocity;
	/*!
	 * Defines how the mass is distributed among the body, i.e. how difficult it is to
	 * rotate around any given axis.
	 */
	internal InertiaMatrix inertiaTensor;
	/*! The body's center of mass. */
	internal Vector4 center;
	/*! The body's mass in kg. A mass of zero represents an immovable object. */
	internal float mass;
	/*!
	 * Reduces the movement of the body over time. A value of 0.1 will remove 10% of
	 * the linear velocity every second.
	 */
	internal float linearDamping;
	/*!
	 * Reduces the movement of the body over time. A value of 0.05 will remove 5% of
	 * the angular velocity every second.
	 */
	internal float angularDamping;
	/*!  */
	internal float timeFactor;
	/*!  */
	internal float gravityFactor;
	/*! How smooth its surfaces is and how easily it will slide along other bodies. */
	internal float friction;
	/*!  */
	internal float rollingFrictionMultiplier;
	/*!
	 * How "bouncy" the body is, i.e. how much energy it has after colliding. Less than
	 * 1.0 loses energy, greater than 1.0 gains energy.
	 *             If the restitution is not 0.0 the object will need extra CPU for all
	 * new collisions.
	 */
	internal float restitution;
	/*! Maximal linear velocity. */
	internal float maxLinearVelocity;
	/*! Maximal angular velocity. */
	internal float maxAngularVelocity;
	/*!
	 * The maximum allowed penetration for this object.
	 *             This is a hint to the engine to see how much CPU the engine should
	 * invest to keep this object from penetrating.
	 *             A good choice is 5% - 20% of the smallest diameter of the object.
	 */
	internal float penetrationDepth;
	/*! Motion system? Overrides Quality when on Keyframed? */
	internal hkMotionType motionSystem;
	/*! The initial deactivator type of the body. */
	internal hkDeactivatorType deactivatorType;
	/*!  */
	internal bool enableDeactivation;
	/*!
	 * How aggressively the engine will try to zero the velocity for slow objects. This
	 * does not save CPU.
	 */
	internal hkSolverDeactivation solverDeactivation;
	/*! The type of interaction with other objects. */
	internal hkQualityType qualityType;
	/*! Unknown. */
	internal float unknownFloat1;
	/*! Unknown. */
	internal Array12<byte> unknownBytes1;
	/*! Unknown. Skyrim only. */
	internal Array4<byte> unknownBytes2;
	/*!  */
	internal uint numConstraints;
	/*!  */
	internal IList<bhkSerializable> constraints;
	/*! 1 = respond to wind */
	internal uint bodyFlags;

	public bhkRigidBody() {
	collisionResponse = hkResponseType.RESPONSE_SIMPLE_CONTACT;
	unusedByte1 = (byte)0;
	processContactCallbackDelay = (ushort)0xffff;
	unknownInt1 = (uint)0;
	unknownInt2 = (uint)0;
	collisionResponse2 = hkResponseType.RESPONSE_SIMPLE_CONTACT;
	unusedByte2 = (byte)0;
	processContactCallbackDelay2 = (ushort)0xffff;
	mass = 1.0f;
	linearDamping = 0.1f;
	angularDamping = 0.05f;
	timeFactor = 1.0f;
	gravityFactor = 1.0f;
	friction = 0.5f;
	rollingFrictionMultiplier = 0.0f;
	restitution = 0.4f;
	maxLinearVelocity = 104.4f;
	maxAngularVelocity = 31.57f;
	penetrationDepth = 0.15f;
	motionSystem = hkMotionType.MO_SYS_DYNAMIC;
	deactivatorType = hkDeactivatorType.DEACTIVATOR_NEVER;
	enableDeactivation = 1;
	solverDeactivation = hkSolverDeactivation.SOLVER_DEACTIVATION_OFF;
	qualityType = hkQualityType.MO_QUAL_FIXED;
	unknownFloat1 = 0.0f;
	numConstraints = (uint)0;
	bodyFlags = (uint)0;
}

/*!
 * Used to determine the type of a particular instance of this object.
 * \return The type constant for the actual type of the object.
 */
public override Type_ GetType() => TYPE;

/*!
 * A factory function used during file reading to create an instance of this type of object.
 * \return A pointer to a newly allocated instance of this type of object.
 */
public static NiObject Create() => new bhkRigidBody();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out collisionResponse, s, info);
	Nif.NifStream(out unusedByte1, s, info);
	Nif.NifStream(out processContactCallbackDelay, s, info);
	if (info.version >= 0x0A010000) {
		Nif.NifStream(out unknownInt1, s, info);
		if ((info.version <= 0x14000005) && ((info.userVersion2 < 16))) {
			Nif.NifStream(out havokFilterCopy.layer_ob, s, info);
		}
		if (((info.version == 0x14020007) && (info.userVersion2 <= 34))) {
			Nif.NifStream(out havokFilterCopy.layer_fo, s, info);
		}
		if (((info.version == 0x14020007) && (info.userVersion2 > 34))) {
			Nif.NifStream(out havokFilterCopy.layer_sk, s, info);
		}
		Nif.NifStream(out havokFilterCopy.flagsAndPartNumber, s, info);
		Nif.NifStream(out havokFilterCopy.group, s, info);
		for (var i2 = 0; i2 < 4; i2++) {
			Nif.NifStream(out unused2[i2], s, info);
		}
	}
	if ((info.version >= 0x0A010000) && ((info.userVersion2 > 34))) {
		Nif.NifStream(out unknownInt2, s, info);
	}
	if (info.version >= 0x0A010000) {
		Nif.NifStream(out collisionResponse2, s, info);
		Nif.NifStream(out unusedByte2, s, info);
		Nif.NifStream(out processContactCallbackDelay2, s, info);
	}
	if ((info.userVersion2 <= 34)) {
		Nif.NifStream(out (uint)unknownInt2, s, info);
	}
	Nif.NifStream(out translation, s, info);
	Nif.NifStream(out rotation.x, s, info);
	Nif.NifStream(out rotation.y, s, info);
	Nif.NifStream(out rotation.z, s, info);
	Nif.NifStream(out rotation.w, s, info);
	Nif.NifStream(out linearVelocity, s, info);
	Nif.NifStream(out angularVelocity, s, info);
	Nif.NifStream(out inertiaTensor, s, info);
	Nif.NifStream(out center, s, info);
	Nif.NifStream(out mass, s, info);
	Nif.NifStream(out linearDamping, s, info);
	Nif.NifStream(out angularDamping, s, info);
	if ((info.userVersion2 > 34)) {
		Nif.NifStream(out timeFactor, s, info);
	}
	if (((info.userVersion2 > 34) && (info.userVersion2 != 130))) {
		Nif.NifStream(out gravityFactor, s, info);
	}
	Nif.NifStream(out friction, s, info);
	if ((info.userVersion2 > 34)) {
		Nif.NifStream(out rollingFrictionMultiplier, s, info);
	}
	Nif.NifStream(out restitution, s, info);
	if (info.version >= 0x0A010000) {
		Nif.NifStream(out maxLinearVelocity, s, info);
		Nif.NifStream(out maxAngularVelocity, s, info);
	}
	if ((info.version >= 0x0A010000) && ((info.userVersion2 != 130))) {
		Nif.NifStream(out penetrationDepth, s, info);
	}
	Nif.NifStream(out motionSystem, s, info);
	if ((info.userVersion2 <= 34)) {
		Nif.NifStream(out deactivatorType, s, info);
	}
	if ((info.userVersion2 > 34)) {
		Nif.NifStream(out enableDeactivation, s, info);
	}
	Nif.NifStream(out solverDeactivation, s, info);
	Nif.NifStream(out qualityType, s, info);
	if ((info.userVersion2 == 130)) {
		Nif.NifStream(out (float)penetrationDepth, s, info);
		Nif.NifStream(out unknownFloat1, s, info);
	}
	for (var i1 = 0; i1 < 12; i1++) {
		Nif.NifStream(out unknownBytes1[i1], s, info);
	}
	if ((info.userVersion2 > 34)) {
		for (var i2 = 0; i2 < 4; i2++) {
			Nif.NifStream(out unknownBytes2[i2], s, info);
		}
	}
	Nif.NifStream(out numConstraints, s, info);
	constraints = new Ref[numConstraints];
	for (var i1 = 0; i1 < constraints.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	if ((info.userVersion2 < 76)) {
		Nif.NifStream(out bodyFlags, s, info);
	}
	if ((info.userVersion2 >= 76)) {
		Nif.NifStream(out (ushort)bodyFlags, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numConstraints = (uint)constraints.Count;
	Nif.NifStream(collisionResponse, s, info);
	Nif.NifStream(unusedByte1, s, info);
	Nif.NifStream(processContactCallbackDelay, s, info);
	if (info.version >= 0x0A010000) {
		Nif.NifStream(unknownInt1, s, info);
		if ((info.version <= 0x14000005) && ((info.userVersion2 < 16))) {
			Nif.NifStream(havokFilterCopy.layer_ob, s, info);
		}
		if (((info.version == 0x14020007) && (info.userVersion2 <= 34))) {
			Nif.NifStream(havokFilterCopy.layer_fo, s, info);
		}
		if (((info.version == 0x14020007) && (info.userVersion2 > 34))) {
			Nif.NifStream(havokFilterCopy.layer_sk, s, info);
		}
		Nif.NifStream(havokFilterCopy.flagsAndPartNumber, s, info);
		Nif.NifStream(havokFilterCopy.group, s, info);
		for (var i2 = 0; i2 < 4; i2++) {
			Nif.NifStream(unused2[i2], s, info);
		}
	}
	if ((info.version >= 0x0A010000) && ((info.userVersion2 > 34))) {
		Nif.NifStream(unknownInt2, s, info);
	}
	if (info.version >= 0x0A010000) {
		Nif.NifStream(collisionResponse2, s, info);
		Nif.NifStream(unusedByte2, s, info);
		Nif.NifStream(processContactCallbackDelay2, s, info);
	}
	if ((info.userVersion2 <= 34)) {
		Nif.NifStream((uint)unknownInt2, s, info);
	}
	Nif.NifStream(translation, s, info);
	Nif.NifStream(rotation.x, s, info);
	Nif.NifStream(rotation.y, s, info);
	Nif.NifStream(rotation.z, s, info);
	Nif.NifStream(rotation.w, s, info);
	Nif.NifStream(linearVelocity, s, info);
	Nif.NifStream(angularVelocity, s, info);
	Nif.NifStream(inertiaTensor, s, info);
	Nif.NifStream(center, s, info);
	Nif.NifStream(mass, s, info);
	Nif.NifStream(linearDamping, s, info);
	Nif.NifStream(angularDamping, s, info);
	if ((info.userVersion2 > 34)) {
		Nif.NifStream(timeFactor, s, info);
	}
	if (((info.userVersion2 > 34) && (info.userVersion2 != 130))) {
		Nif.NifStream(gravityFactor, s, info);
	}
	Nif.NifStream(friction, s, info);
	if ((info.userVersion2 > 34)) {
		Nif.NifStream(rollingFrictionMultiplier, s, info);
	}
	Nif.NifStream(restitution, s, info);
	if (info.version >= 0x0A010000) {
		Nif.NifStream(maxLinearVelocity, s, info);
		Nif.NifStream(maxAngularVelocity, s, info);
	}
	if ((info.version >= 0x0A010000) && ((info.userVersion2 != 130))) {
		Nif.NifStream(penetrationDepth, s, info);
	}
	Nif.NifStream(motionSystem, s, info);
	if ((info.userVersion2 <= 34)) {
		Nif.NifStream(deactivatorType, s, info);
	}
	if ((info.userVersion2 > 34)) {
		Nif.NifStream(enableDeactivation, s, info);
	}
	Nif.NifStream(solverDeactivation, s, info);
	Nif.NifStream(qualityType, s, info);
	if ((info.userVersion2 == 130)) {
		Nif.NifStream((float)penetrationDepth, s, info);
		Nif.NifStream(unknownFloat1, s, info);
	}
	for (var i1 = 0; i1 < 12; i1++) {
		Nif.NifStream(unknownBytes1[i1], s, info);
	}
	if ((info.userVersion2 > 34)) {
		for (var i2 = 0; i2 < 4; i2++) {
			Nif.NifStream(unknownBytes2[i2], s, info);
		}
	}
	Nif.NifStream(numConstraints, s, info);
	for (var i1 = 0; i1 < constraints.Count; i1++) {
		WriteRef((NiObject)constraints[i1], s, info, link_map, missing_link_stack);
	}
	if ((info.userVersion2 < 76)) {
		Nif.NifStream(bodyFlags, s, info);
	}
	if ((info.userVersion2 >= 76)) {
		Nif.NifStream((ushort)bodyFlags, s, info);
	}

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	uint array_output_count = 0;
	s.Append(base.AsString());
	numConstraints = (uint)constraints.Count;
	s.AppendLine($"  Collision Response:  {collisionResponse}");
	s.AppendLine($"  Unused Byte 1:  {unusedByte1}");
	s.AppendLine($"  Process Contact Callback Delay:  {processContactCallbackDelay}");
	s.AppendLine($"  Unknown Int 1:  {unknownInt1}");
	s.AppendLine($"  Layer:  {havokFilterCopy.layer_ob}");
	s.AppendLine($"  Layer:  {havokFilterCopy.layer_fo}");
	s.AppendLine($"  Layer:  {havokFilterCopy.layer_sk}");
	s.AppendLine($"  Flags and Part Number:  {havokFilterCopy.flagsAndPartNumber}");
	s.AppendLine($"  Group:  {havokFilterCopy.group}");
	array_output_count = 0;
	for (var i1 = 0; i1 < 4; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Unused 2[{i1}]:  {unused2[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Unknown Int 2:  {unknownInt2}");
	s.AppendLine($"  Collision Response 2:  {collisionResponse2}");
	s.AppendLine($"  Unused Byte 2:  {unusedByte2}");
	s.AppendLine($"  Process Contact Callback Delay 2:  {processContactCallbackDelay2}");
	s.AppendLine($"  Translation:  {translation}");
	s.AppendLine($"  x:  {rotation.x}");
	s.AppendLine($"  y:  {rotation.y}");
	s.AppendLine($"  z:  {rotation.z}");
	s.AppendLine($"  w:  {rotation.w}");
	s.AppendLine($"  Linear Velocity:  {linearVelocity}");
	s.AppendLine($"  Angular Velocity:  {angularVelocity}");
	s.AppendLine($"  Inertia Tensor:  {inertiaTensor}");
	s.AppendLine($"  Center:  {center}");
	s.AppendLine($"  Mass:  {mass}");
	s.AppendLine($"  Linear Damping:  {linearDamping}");
	s.AppendLine($"  Angular Damping:  {angularDamping}");
	s.AppendLine($"  Time Factor:  {timeFactor}");
	s.AppendLine($"  Gravity Factor:  {gravityFactor}");
	s.AppendLine($"  Friction:  {friction}");
	s.AppendLine($"  Rolling Friction Multiplier:  {rollingFrictionMultiplier}");
	s.AppendLine($"  Restitution:  {restitution}");
	s.AppendLine($"  Max Linear Velocity:  {maxLinearVelocity}");
	s.AppendLine($"  Max Angular Velocity:  {maxAngularVelocity}");
	s.AppendLine($"  Penetration Depth:  {penetrationDepth}");
	s.AppendLine($"  Motion System:  {motionSystem}");
	s.AppendLine($"  Deactivator Type:  {deactivatorType}");
	s.AppendLine($"  Enable Deactivation:  {enableDeactivation}");
	s.AppendLine($"  Solver Deactivation:  {solverDeactivation}");
	s.AppendLine($"  Quality Type:  {qualityType}");
	s.AppendLine($"  Unknown Float 1:  {unknownFloat1}");
	array_output_count = 0;
	for (var i1 = 0; i1 < 12; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Unknown Bytes 1[{i1}]:  {unknownBytes1[i1]}");
		array_output_count++;
	}
	array_output_count = 0;
	for (var i1 = 0; i1 < 4; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Unknown Bytes 2[{i1}]:  {unknownBytes2[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Constraints:  {numConstraints}");
	array_output_count = 0;
	for (var i1 = 0; i1 < constraints.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Constraints[{i1}]:  {constraints[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Body Flags:  {bodyFlags}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < constraints.Count; i1++) {
		constraints[i1] = FixLink<bhkSerializable>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < constraints.Count; i1++) {
		if (constraints[i1] != null)
			refs.Add((NiObject)constraints[i1]);
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < constraints.Count; i1++) {
	}
	return ptrs;
}

//--BEGIN:FILE FOOT--//
        /*!
         * Gets or sets what appears to be a copy of the layer value.
         * \param[in] value The new duplicate layer value.
         */
        public HavokFilter HavokFilterCopy
        {
            get => havokFilterCopy;
            set => havokFilterCopy = value;
        }

        /*!
         * Gets or Sets a new translation for this rigid body.
         * \param[in] value  The new translation for this rigid body.
         */
        public Vector4 Translation
        {
            get => translation;
            set => translation = value;
        }

        /*!
         * Gets or Sets a new rotation for this rigid body.
         * \param[in] value The new rotation for this rigid body.
         */
        public hkQuaternion Rotation
        {
            get => rotation;
            set => rotation = value;
        }

        /*!
         * Gets or Sets a new linear velocity for this rigid body.
         * \param[in] value The new linear velocity for this rigid body.
         */
        public Vector4 LinearVelocity
        {
            get => linearVelocity;
            set => linearVelocity = value;
        }

        /*!
         * Gets or Sets a new angular velocity for this rigid body.
         * \param[in] value The new angular velocity for this rigid body.
         */
        public Vector4 AngularVelocity
        {
            get => angularVelocity;
            set => angularVelocity = value;
        }

        /*!
         * Gets or Sets a new inertia for this rigid body.
         * \param[in] value The new inertia for this rigid body.
         */
        public InertiaMatrix InertiaTensor
        {
            get => inertiaTensor;
            set => inertiaTensor = value;
        }

        /*!
         * Gets or Sets a new center point for this rigid body.
         * \param[in] value The new center point for this rigid body.
         */
        public Vector4 Center
        {
            get => center;
            set => center = value;
        }

        /*!
         * Gets or Sets a new mass for this rigid body.
         * \param[in] value The new mass for this rigid body.
         */
        public float Mass
        {
            get => mass;
            set => mass = value;
        }

        /*!
         * Gets or Sets a new linear damping level for this rigid body.
         * \param[in] value The new linear damping level for this rigid body.
         */
        public float SetLinearDamping
        {
            get => linearDamping;
            set => linearDamping = value;
        }

        /*!
         * Get or Sets a new angular damping level for this rigid body.
         * \param[in] value The new angular damping level for this rigid body.
         */
        public float AngularDamping
        {
            get => angularDamping;
            set => angularDamping = value;
        }

        /*!
         * Sets a new friction for this rigid body.
         * \param[in] value The new friction for this rigid body.
         */
        public float Friction
        {
            get => friction;
            set => friction = value;
        }

        /*!
         * Gets or Sets a new restitution for this rigid body.
         * \param[in] value The new restitution for this rigid body.
         */
        public float Restitution
        {
            get => restitution;
            set => restitution = value;
        }

        /*!
         * Gets or Sets a new maximum linear velocit for this rigid body.
         * \param[in] value The new maximum linear velocity for this rigid body.
         */
        public float MaxLinearVelocity
        {
            get => maxLinearVelocity;
            set => maxLinearVelocity = value;
        }

        /*!
         * Gets or Sets a new maximum angular velocity for this rigid body.
         * \param[in] value The new maximum angular velocit for this rigid body.
         */
        public float MaxAngularVelocity
        {
            get => maxAngularVelocity;
            set => maxAngularVelocity = value;
        }

        /*!
         * Get or Sets a new allowable penetration depth for this rigid body.
         * \param[in] value The new allowable penetration depth for this rigid body.
         */
        public float PenetrationDepth
        {
            get => penetrationDepth;
            set => penetrationDepth = value;
        }

        /*!
         * Gets or Sets the current motion type for this rigid body.  Seems to override motion quality when set to keyframed.
         * \return The current motion system setting of this rigid body.
         */
        public hkMotionType MotionType
        {
            get => motionSystem;
            set => motionSystem = value;
        }

        /*!
         * Gets or Sets the quality of the calculations used to detect collisions for this object.  Essentially, the faster the object goes, the higher quality of motion it will require.
         * \return The current motion quality setting of this rigid body.
         */
        public hkQualityType QualityType
        {
            get => qualityType;
            set => qualityType = value;
        }

        // The initial deactivator type of the body.
        // \param[in] value The new value.
        public hkDeactivatorType DeactivatorType
        {
            get => deactivatorType;
            set => deactivatorType = value;
        }

        // Usually set to 1 for fixed objects, or set to 2 for moving ones.  Seems to
        // always be same as Unknown Byte 1.
        // \param[in] value The new value.
        public hkSolverDeactivation SolverDeactivation
        {
            get => solverDeactivation;
            set => solverDeactivation = value;
        }

        /*!
         * Adds a constraint to this bhkRigidBody.
         */
        public void AddConstraint(bhkSerializable obj) => constraints.Add(obj);

        /*!
         * Removes a constraint from this bhkRigidBody.
         */
        public void RemoveConstraint(bhkSerializable obj) => constraints.Remove(obj);

        /*!
         * Removes all constraints from this bhkRigidBody.
         */
        public void ClearConstraints() => constraints.Clear();

        /*!
         * Retrieves all the constraints attached to this bhkRigidBody.
         */
        public IList<bhkSerializable> Constraints => constraints;

        // Apply scale factor <scale> on data.
        // \param[in] scale Factor to scale by
        public void ApplyScale(float scale)
        {
            // apply scale on transform
            translation *= scale;
            // apply scale on center of gravity
            center *= scale;
            // apply scale on inertia tensor
            inertiaTensor *= (float)Math.Pow(scale, 2.0f);
            //# apply scale on all blocks down the hierarchy
            //ApplyScale(scale)
        }

        // Look at all the objects under this rigid body and update the mass
        //  center of gravity, and inertia tensor accordingly. If the mass parameter
        //  is given then the density argument is ignored.
        public void UpdateMassProperties(float density = 1.0f, bool solid = true, float mass_ = 0.0f)
        {
            // Look at all the objects under this rigid body and update the mass
            //  center of gravity, and inertia tensor accordingly. If the C{mass} parameter
            //  is given then the C{density} argument is ignored.
            if (mass_ != 0.0f)
                density = 1.0f;
            if (shape != null)
            {
                shape.CalcMassProperties(density, solid, out mass, out var volume, out var center, out var inertia);
                if (mass_ != 0.0f)
                {
                    var mass_correction = mass != 0.0f ? mass_ / mass : 1.0f;
                    mass = mass_;
                    inertia *= mass_correction;
                }
            }
        }
//--END:CUSTOM--//

}

}