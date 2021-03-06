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

    /*! Particle emitter that uses points on a specified mesh to emit from. */
    public class NiPSysMeshEmitter : NiPSysEmitter
    {
        //Definition of TYPE constant
        public static readonly Type_ TYPE = new Type_("NiPSysMeshEmitter", NiPSysEmitter.TYPE);
        /*!  */
        internal uint numEmitterMeshes;
        /*! The meshes which are emitted from. */
        internal IList<NiAVObject> emitterMeshes;
        /*! The method by which the initial particle velocity will be computed. */
        internal VelocityType initialVelocityType;
        /*! The manner in which particles are emitted from the Emitter Meshes. */
        internal EmitFrom emissionType;
        /*! The emission axis if VELOCITY_USE_DIRECTION. */
        internal Vector3 emissionAxis;

        public NiPSysMeshEmitter()
        {
            numEmitterMeshes = (uint)0;
            initialVelocityType = (VelocityType)0;
            emissionType = (EmitFrom)0;
            emissionAxis = 1.0, 0.0, 0.0;
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
        public static NiObject Create() => new NiPSysMeshEmitter();

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
        {

            uint block_num;
            base.Read(s, link_stack, info);
            Nif.NifStream(out numEmitterMeshes, s, info);
            emitterMeshes = new *[numEmitterMeshes];
            for (var i1 = 0; i1 < emitterMeshes.Count; i1++)
            {
                Nif.NifStream(out block_num, s, info);
                link_stack.Add(block_num);
            }
            Nif.NifStream(out initialVelocityType, s, info);
            Nif.NifStream(out emissionType, s, info);
            Nif.NifStream(out emissionAxis, s, info);

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.Write(s, link_map, missing_link_stack, info);
            numEmitterMeshes = (uint)emitterMeshes.Count;
            Nif.NifStream(numEmitterMeshes, s, info);
            for (var i1 = 0; i1 < emitterMeshes.Count; i1++)
            {
                WriteRef((NiObject)emitterMeshes[i1], s, info, link_map, missing_link_stack);
            }
            Nif.NifStream(initialVelocityType, s, info);
            Nif.NifStream(emissionType, s, info);
            Nif.NifStream(emissionAxis, s, info);

        }

        /*!
         * Summarizes the information contained in this object in English.
         * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
         * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
         */
        public override string AsString(bool verbose = false)
        {

            var s = new System.Text.StringBuilder();
            uint array_output_count = 0;
            s.Append(base.AsString());
            numEmitterMeshes = (uint)emitterMeshes.Count;
            s.AppendLine($"  Num Emitter Meshes:  {numEmitterMeshes}");
            array_output_count = 0;
            for (var i1 = 0; i1 < emitterMeshes.Count; i1++)
            {
                if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                {
                    s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
                    break;
                }
                if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                {
                    break;
                }
                s.AppendLine($"    Emitter Meshes[{i1}]:  {emitterMeshes[i1]}");
                array_output_count++;
            }
            s.AppendLine($"  Initial Velocity Type:  {initialVelocityType}");
            s.AppendLine($"  Emission Type:  {emissionType}");
            s.AppendLine($"  Emission Axis:  {emissionAxis}");
            return s.ToString();

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.FixLinks(objects, link_stack, missing_link_stack, info);
            for (var i1 = 0; i1 < emitterMeshes.Count; i1++)
            {
                emitterMeshes[i1] = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);
            }

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetRefs()
        {
            var refs = base.GetRefs();
            for (var i1 = 0; i1 < emitterMeshes.Count; i1++)
            {
            }
            return refs;
        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetPtrs()
        {
            var ptrs = base.GetPtrs();
            for (var i1 = 0; i1 < emitterMeshes.Count; i1++)
            {
                if (emitterMeshes[i1] != null)
                    ptrs.Add((NiObject)emitterMeshes[i1]);
            }
            return ptrs;
        }

        //--BEGIN:FILE FOOT--//

        /*!
         * Adds a single geometry to the collection. The collection will expand if necessary.
         * \param[in] mesh The shape to add to the collection.
         */
        public bool AddEmitterMesh(NiTriBasedGeom mesh)
        {
            var meshes = emitterMeshes;
            if (!emitterMeshes.Contains(mesh))
            {
                meshes.Add(mesh);
                numEmitterMeshes++;
                return true;
            }
            return false;
        }

        /*!
         * Remove a single geometry from the collection.
         * \param[in] mesh The shape remove from the collection.
         */
        public bool RemoveEmitterMesh(NiTriBasedGeom mesh)
        {
            var meshes = emitterMeshes;
            if (emitterMeshes.Contains(mesh))
            {
                meshes.Remove(mesh);
                numEmitterMeshes--;
                return true;
            }
            return false;
        }

        /*!
         * Replace a single geometry by another in the specified shape group.
         * \param[in] newmesh The geometry put into the collection.
         * \param[in] oldmesh The geometry remove from collection.
         */
        public bool ReplaceEmitterMesh(NiTriBasedGeom newmesh, NiTriBasedGeom oldmesh)
        {
            var meshes = emitterMeshes;
            var itr = meshes.IndexOf(oldmesh);
            if (itr != -1)
            {
                meshes.RemoveAt(itr);
                meshes.Insert(itr, newmesh);
                return true;
            }
            return false;
        }
        //--END:CUSTOM--//
    }

}