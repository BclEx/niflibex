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
 * Abstract base class that represents light sources in a scene graph.
 *         For Bethesda Stream 130 (FO4), NiLight now directly inherits from
 * NiAVObject.
 */
public class NiLight : NiDynamicEffect {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiLight", NiDynamicEffect.TYPE);
	/*! Scales the overall brightness of all light components. */
	internal float dimmer;
	/*!  */
	internal Color3 ambientColor;
	/*!  */
	internal Color3 diffuseColor;
	/*!  */
	internal Color3 specularColor;

	public NiLight() {
	dimmer = 1.0f;
	ambientColor = 0.0, 0.0, 0.0;
	diffuseColor = 0.0, 0.0, 0.0;
	specularColor = 0.0, 0.0, 0.0;
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
public static NiObject Create() => new NiLight();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out dimmer, s, info);
	Nif.NifStream(out ambientColor, s, info);
	Nif.NifStream(out diffuseColor, s, info);
	Nif.NifStream(out specularColor, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(dimmer, s, info);
	Nif.NifStream(ambientColor, s, info);
	Nif.NifStream(diffuseColor, s, info);
	Nif.NifStream(specularColor, s, info);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Dimmer:  {dimmer}");
	s.AppendLine($"  Ambient Color:  {ambientColor}");
	s.AppendLine($"  Diffuse Color:  {diffuseColor}");
	s.AppendLine($"  Specular Color:  {specularColor}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}

//--BEGIN:FILE FOOT--//
        /*!
         * Gets or sets the dimmer value for this light.  Used to turn the light intensity up or down.
         * \param[in] value The new dimmer value.
         */
        public float Dimmer
        {
            get => dimmer;
            set => dimmer = value;
        }

        /*!
         * Gets or sets the ambient color of this light.
         * \param[in] value The new ambient color.
         */
        public Color3 AmbientColor
        {
            get => ambientColor;
            set => ambientColor = value;
        }

        /*!
         * Gets or sets the diffuse color of this light.
         * \param[in] value The new diffuse color.
         */
        public Color3 DiffuseColor
        {
            get => diffuseColor;
            set => diffuseColor = value;
        }

        /*!
         * Gets or sets the specular color of this light.
         * \param[in] value The new specular color.
         */
        public Color3 SpecularColor
        {
            get => specularColor;
            set => specularColor = value;
        }
//--END:CUSTOM--//

}

}