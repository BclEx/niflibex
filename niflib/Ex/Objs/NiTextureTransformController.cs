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
 * Used to animate a single member of an NiTextureTransform.
 *         NiInterpController::GetCtlrID() string formats:
 *             ['%1-%2-TT_TRANSLATE_U', '%1-%2-TT_TRANSLATE_V', '%1-%2-TT_ROTATE',
 * '%1-%2-TT_SCALE_U', '%1-%2-TT_SCALE_V']
 *         (Depending on "Operation" enumeration, %1 = Value of "Shader Map", %2 =
 * Value of "Texture Slot")
 */
public class NiTextureTransformController : NiFloatInterpController {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiTextureTransformController", NiFloatInterpController.TYPE);
	/*! Is the target map a shader map? */
	internal bool shaderMap;
	/*! The target texture slot. */
	internal TexType textureSlot;
	/*! Controls which aspect of the texture transform to modify. */
	internal TransformMember operation;
	/*!  */
	internal NiFloatData data;

	public NiTextureTransformController() {
	shaderMap = false;
	textureSlot = (TexType)0;
	operation = (TransformMember)0;
	data = null;
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
public static NiObject Create() => new NiTextureTransformController();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out shaderMap, s, info);
	Nif.NifStream(out textureSlot, s, info);
	Nif.NifStream(out operation, s, info);
	if (info.version <= 0x0A010067) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(shaderMap, s, info);
	Nif.NifStream(textureSlot, s, info);
	Nif.NifStream(operation, s, info);
	if (info.version <= 0x0A010067) {
		WriteRef((NiObject)data, s, info, link_map, missing_link_stack);
	}

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Shader Map:  {shaderMap}");
	s.AppendLine($"  Texture Slot:  {textureSlot}");
	s.AppendLine($"  Operation:  {operation}");
	s.AppendLine($"  Data:  {data}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	if (info.version <= 0x0A010067) {
		data = FixLink<NiFloatData>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (data != null)
		refs.Add((NiObject)data);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}
        //--BEGIN:FILE FOOT--//
        /*
         * Gets or sets the texture slot that will be the target of this texture transform controller.
         * \param[in] n The new texture slot that will be animated by this controller.
         */
        public TexType TargetTextureSlot
        {
            get => textureSlot;
            set => textureSlot = value;
        }

        /*
         * Gets or sets the type of texture transformation that this controller applies.
         * \param[in] n The new way that this controller will animate the target texture.
         */
        public TransformMember TextureTransformType
        {
            get => operation;
            set => operation = value;
        }

        /*!
         * Gets or sets the float data used by this controller.
         * \param[in] n The new float data.
         */
        public NiFloatData Data
        {
            get => data;
            set => data = value;
        }

        /*!
         * This function will adjust the times in all the keys in the data objects
         * referenced by this controller and any of its interpolators such that the
         * phase will equal 0 and frequency will equal one.  In other words, it
         * will cause the key times to be in seconds starting from zero.
         */
        public virtual void NormalizeKeys()
        {
            // If data is not the same as The interpolator data, which will be
            // normalized by the call to NiSingleInterpController's version of
            // this function, then normalize it here.
            var interp = interpolator as NiFloatInterpolator;
            if (interp.Data != data && data != null)
                data.NormalizeKeys(phase, frequency);
            //Call the parent version of this function to finish up
            base.NormalizeKeys();
        }
        //--END:CUSTOM--//

    }

}