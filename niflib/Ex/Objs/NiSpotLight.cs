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


namespace Niflib
{

    /*! A spot. */
    public class NiSpotLight : NiPointLight
    {
        //Definition of TYPE constant
        public static readonly Type_ TYPE = new Type_("NiSpotLight", NiPointLight.TYPE);
        /*!  */
        internal float outerSpotAngle;
        /*!  */
        internal float innerSpotAngle;
        /*! Describes the distribution of light. (see: glLight) */
        internal float exponent;

        public NiSpotLight()
        {
            outerSpotAngle = 0.0f;
            innerSpotAngle = 0.0f;
            exponent = 1.0f;
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
        public static NiObject Create() => new NiSpotLight();

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
        {

            base.Read(s, link_stack, info);
            Nif.NifStream(out outerSpotAngle, s, info);
            if (info.version >= 0x14020005)
            {
                Nif.NifStream(out innerSpotAngle, s, info);
            }
            Nif.NifStream(out exponent, s, info);

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.Write(s, link_map, missing_link_stack, info);
            Nif.NifStream(outerSpotAngle, s, info);
            if (info.version >= 0x14020005)
            {
                Nif.NifStream(innerSpotAngle, s, info);
            }
            Nif.NifStream(exponent, s, info);

        }

        /*!
         * Summarizes the information contained in this object in English.
         * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
         * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
         */
        public override string AsString(bool verbose = false)
        {

            var s = new System.Text.StringBuilder();
            s.Append(base.AsString());
            s.AppendLine($"  Outer Spot Angle:  {outerSpotAngle}");
            s.AppendLine($"  Inner Spot Angle:  {innerSpotAngle}");
            s.AppendLine($"  Exponent:  {exponent}");
            return s.ToString();

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.FixLinks(objects, link_stack, missing_link_stack, info);

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetRefs()
        {
            var refs = base.GetRefs();
            return refs;
        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetPtrs()
        {
            var ptrs = base.GetPtrs();
            return ptrs;
        }
        //--BEGIN:FILE FOOT--//
        /*!
         * Gets or sets the opening angle of the spot light.
         * \param[in] value The new cutoff angle.
         */
        public float OuterSpotAngle
        {
            get => outerSpotAngle;
            set => outerSpotAngle = value;
        }

        /*!
         * Gets or sets the opening angle of the spot light.
         * \param[in] value The new cutoff angle.
         */
        public float InnerSpotAngle
        {
            get => innerSpotAngle;
            set => innerSpotAngle = value;
        }

        /*!
         * Sets the exponent of this spot light.  This describes the distribution of light.  See glLight in OpenGL.
         * \param[in] value The new exponent value.
         */
        public float Exponent
        {
            get => exponent;
            set => exponent = value;
        }
        //--END:CUSTOM--//

    }

}