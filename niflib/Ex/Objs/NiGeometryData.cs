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

/*! Mesh data: vertices, vertex normals, etc. */
public class NiGeometryData : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiGeometryData", NiObject.TYPE);
	/*! Always zero. */
	internal int groupId;
	/*! Number of vertices. */
	internal ushort numVertices;
	/*! Bethesda uses this for max number of particles in NiPSysData. */
	internal ushort bsMaxVertices;
	/*! Used with NiCollision objects when OBB or TRI is set. */
	internal byte keepFlags;
	/*! Unknown. */
	internal byte compressFlags;
	/*! Is the vertex array present? (Always non-zero.) */
	internal bool hasVertices;
	/*! The mesh vertices. */
	internal IList<Vector3> vertices;
	/*!  */
	internal VectorFlags vectorFlags;
	/*!  */
	internal BSVectorFlags bsVectorFlags;
	/*!  */
	internal uint materialCrc;
	/*!
	 * Do we have lighting normals? These are essential for proper lighting: if not
	 * present, the model will only be influenced by ambient light.
	 */
	internal bool hasNormals;
	/*! The lighting normals. */
	internal IList<Vector3> normals;
	/*! Tangent vectors. */
	internal IList<Vector3> tangents;
	/*! Bitangent vectors. */
	internal IList<Vector3> bitangents;
	/*!
	 * Center of the bounding box (smallest box that contains all vertices) of the
	 * mesh.
	 */
	internal Vector3 center;
	/*!
	 * Radius of the mesh: maximal Euclidean distance between the center and all
	 * vertices.
	 */
	internal float radius;
	/*! Unknown, always 0? */
	internal Array13<short> unknown13Shorts;
	/*!
	 * Do we have vertex colors? These are usually used to fine-tune the lighting of
	 * the model.
	 * 
	 *             Note: how vertex colors influence the model can be controlled by
	 * having a NiVertexColorProperty object as a property child of the root node. If
	 * this property object is not present, the vertex colors fine-tune lighting.
	 * 
	 *             Note 2: set to either 0 or 0xFFFFFFFF for NifTexture compatibility.
	 */
	internal bool hasVertexColors;
	/*! The vertex colors. */
	internal IList<Color4> vertexColors;
	/*!
	 * The lower 6 (or less?) bits of this field represent the number of UV texture
	 * sets. The other bits are probably flag bits. For versions 10.1.0.0 and up, if
	 * bit 12 is set then extra vectors are present after the normals.
	 */
	internal ushort numUvSets;
	/*!
	 * Do we have UV coordinates?
	 * 
	 *             Note: for compatibility with NifTexture, set this value to either
	 * 0x00000000 or 0xFFFFFFFF.
	 */
	internal bool hasUv;
	/*!
	 * The UV texture coordinates. They follow the OpenGL standard: some programs may
	 * require you to flip the second coordinate.
	 */
	internal IList<TexCoord[]> uvSets;
	/*! Consistency Flags */
	internal ConsistencyType consistencyFlags;
	/*! Unknown. */
	internal AbstractAdditionalGeometryData additionalData;

	public NiGeometryData() {
	groupId = (int)0;
	numVertices = (ushort)0;
	bsMaxVertices = (ushort)0;
	keepFlags = (byte)0;
	compressFlags = (byte)0;
	hasVertices = 1;
	vectorFlags = (VectorFlags)0;
	bsVectorFlags = (BSVectorFlags)0;
	materialCrc = (uint)0;
	hasNormals = false;
	radius = 0.0f;
	hasVertexColors = false;
	numUvSets = (ushort)0;
	hasUv = false;
	consistencyFlags = ConsistencyType.CT_MUTABLE;
	additionalData = null;
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
public static NiObject Create() => new NiGeometryData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	if (info.version >= 0x0A010072) {
		Nif.NifStream(out groupId, s, info);
	}
	if ((!IsDerivedType(NiPSysData.TYPE))) {
		Nif.NifStream(out numVertices, s, info);
	}
	if ((info.userVersion2 < 34)) {
		if (IsDerivedType(NiPSysData.TYPE)) {
			Nif.NifStream(out (ushort)numVertices, s, info);
		}
	}
	if ((info.userVersion2 >= 34)) {
		if (IsDerivedType(NiPSysData.TYPE)) {
			Nif.NifStream(out bsMaxVertices, s, info);
		}
	}
	if (info.version >= 0x0A010000) {
		Nif.NifStream(out keepFlags, s, info);
		Nif.NifStream(out compressFlags, s, info);
	}
	Nif.NifStream(out hasVertices, s, info);
	if (hasVertices) {
		vertices = new Vector3[numVertices];
		for (var i2 = 0; i2 < vertices.Count; i2++) {
			Nif.NifStream(out vertices[i2], s, info);
		}
	}
	if ((info.version >= 0x0A000100) && ((!((info.version == 0x14020007) && (info.userVersion2 > 0))))) {
		Nif.NifStream(out vectorFlags, s, info);
	}
	if (((info.version == 0x14020007) && (info.userVersion2 > 0))) {
		Nif.NifStream(out bsVectorFlags, s, info);
	}
	if ((info.version >= 0x14020007) && (info.version <= 0x14020007) && (info.userVersion == 12)) {
		Nif.NifStream(out materialCrc, s, info);
	}
	Nif.NifStream(out hasNormals, s, info);
	if (hasNormals) {
		normals = new Vector3[numVertices];
		for (var i2 = 0; i2 < normals.Count; i2++) {
			Nif.NifStream(out normals[i2], s, info);
		}
	}
	if (info.version >= 0x0A010000) {
		if ((hasNormals && ((vectorFlags | bsVectorFlags) & 4096))) {
			tangents = new Vector3[numVertices];
			for (var i3 = 0; i3 < tangents.Count; i3++) {
				Nif.NifStream(out tangents[i3], s, info);
			}
			bitangents = new Vector3[numVertices];
			for (var i3 = 0; i3 < bitangents.Count; i3++) {
				Nif.NifStream(out bitangents[i3], s, info);
			}
		}
	}
	Nif.NifStream(out center, s, info);
	Nif.NifStream(out radius, s, info);
	if ((info.version >= 0x14030009) && (info.version <= 0x14030009) && (info.userVersion == 131072)) {
		for (var i2 = 0; i2 < 13; i2++) {
			Nif.NifStream(out unknown13Shorts[i2], s, info);
		}
	}
	Nif.NifStream(out hasVertexColors, s, info);
	if (hasVertexColors) {
		vertexColors = new Color4[numVertices];
		for (var i2 = 0; i2 < vertexColors.Count; i2++) {
			Nif.NifStream(out vertexColors[i2], s, info);
		}
	}
	if (info.version <= 0x04020200) {
		Nif.NifStream(out numUvSets, s, info);
	}
	if (info.version <= 0x04000002) {
		Nif.NifStream(out hasUv, s, info);
	}
	uvSets = new TexCoord[((numUvSets & 63) | ((vectorFlags & 63) | (bsVectorFlags & 1)))];
	for (var i1 = 0; i1 < uvSets.Count; i1++) {
		uvSets[i1].Resize(numVertices);
		for (var i2 = 0; i2 < uvSets[i1].Count; i2++) {
			Nif.NifStream(out uvSets[i1][i2], s, info);
		}
	}
	if (info.version >= 0x0A000100) {
		Nif.NifStream(out consistencyFlags, s, info);
	}
	if (info.version >= 0x14000004) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numVertices = (ushort)vertices.Count;
	if (info.version >= 0x0A010072) {
		Nif.NifStream(groupId, s, info);
	}
	if ((!IsDerivedType(NiPSysData.TYPE))) {
		Nif.NifStream(numVertices, s, info);
	}
	if ((info.userVersion2 < 34)) {
		if (IsDerivedType(NiPSysData.TYPE)) {
			Nif.NifStream((ushort)numVertices, s, info);
		}
	}
	if ((info.userVersion2 >= 34)) {
		if (IsDerivedType(NiPSysData.TYPE)) {
			Nif.NifStream(bsMaxVertices, s, info);
		}
	}
	if (info.version >= 0x0A010000) {
		Nif.NifStream(keepFlags, s, info);
		Nif.NifStream(compressFlags, s, info);
	}
	Nif.NifStream(hasVertices, s, info);
	if (hasVertices) {
		for (var i2 = 0; i2 < vertices.Count; i2++) {
			Nif.NifStream(vertices[i2], s, info);
		}
	}
	if ((info.version >= 0x0A000100) && ((!((info.version == 0x14020007) && (info.userVersion2 > 0))))) {
		Nif.NifStream(vectorFlags, s, info);
	}
	if (((info.version == 0x14020007) && (info.userVersion2 > 0))) {
		Nif.NifStream(bsVectorFlags, s, info);
	}
	if ((info.version >= 0x14020007) && (info.version <= 0x14020007) && (info.userVersion == 12)) {
		Nif.NifStream(materialCrc, s, info);
	}
	Nif.NifStream(hasNormals, s, info);
	if (hasNormals) {
		for (var i2 = 0; i2 < normals.Count; i2++) {
			Nif.NifStream(normals[i2], s, info);
		}
	}
	if (info.version >= 0x0A010000) {
		if ((hasNormals && ((vectorFlags | bsVectorFlags) & 4096))) {
			for (var i3 = 0; i3 < tangents.Count; i3++) {
				Nif.NifStream(tangents[i3], s, info);
			}
			for (var i3 = 0; i3 < bitangents.Count; i3++) {
				Nif.NifStream(bitangents[i3], s, info);
			}
		}
	}
	Nif.NifStream(center, s, info);
	Nif.NifStream(radius, s, info);
	if ((info.version >= 0x14030009) && (info.version <= 0x14030009) && (info.userVersion == 131072)) {
		for (var i2 = 0; i2 < 13; i2++) {
			Nif.NifStream(unknown13Shorts[i2], s, info);
		}
	}
	Nif.NifStream(hasVertexColors, s, info);
	if (hasVertexColors) {
		for (var i2 = 0; i2 < vertexColors.Count; i2++) {
			Nif.NifStream(vertexColors[i2], s, info);
		}
	}
	if (info.version <= 0x04020200) {
		Nif.NifStream(numUvSets, s, info);
	}
	if (info.version <= 0x04000002) {
		Nif.NifStream(hasUv, s, info);
	}
	for (var i1 = 0; i1 < uvSets.Count; i1++) {
		for (var i2 = 0; i2 < uvSets[i1].Count; i2++) {
			Nif.NifStream(uvSets[i1][i2], s, info);
		}
	}
	if (info.version >= 0x0A000100) {
		Nif.NifStream(consistencyFlags, s, info);
	}
	if (info.version >= 0x14000004) {
		WriteRef((NiObject)additionalData, s, info, link_map, missing_link_stack);
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
	numVertices = (ushort)vertices.Count;
	s.AppendLine($"  Group ID:  {groupId}");
	if ((!IsDerivedType(NiPSysData.TYPE))) {
		s.AppendLine($"    Num Vertices:  {numVertices}");
	}
	if (IsDerivedType(NiPSysData.TYPE)) {
		s.AppendLine($"    BS Max Vertices:  {bsMaxVertices}");
	}
	s.AppendLine($"  Keep Flags:  {keepFlags}");
	s.AppendLine($"  Compress Flags:  {compressFlags}");
	s.AppendLine($"  Has Vertices:  {hasVertices}");
	if (hasVertices) {
		array_output_count = 0;
		for (var i2 = 0; i2 < vertices.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Vertices[{i2}]:  {vertices[i2]}");
			array_output_count++;
		}
	}
	s.AppendLine($"  Vector Flags:  {vectorFlags}");
	s.AppendLine($"  BS Vector Flags:  {bsVectorFlags}");
	s.AppendLine($"  Material CRC:  {materialCrc}");
	s.AppendLine($"  Has Normals:  {hasNormals}");
	if (hasNormals) {
		array_output_count = 0;
		for (var i2 = 0; i2 < normals.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Normals[{i2}]:  {normals[i2]}");
			array_output_count++;
		}
	}
	if ((hasNormals && ((vectorFlags | bsVectorFlags) & 4096))) {
		array_output_count = 0;
		for (var i2 = 0; i2 < tangents.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Tangents[{i2}]:  {tangents[i2]}");
			array_output_count++;
		}
		array_output_count = 0;
		for (var i2 = 0; i2 < bitangents.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Bitangents[{i2}]:  {bitangents[i2]}");
			array_output_count++;
		}
	}
	s.AppendLine($"  Center:  {center}");
	s.AppendLine($"  Radius:  {radius}");
	array_output_count = 0;
	for (var i1 = 0; i1 < 13; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Unknown 13 shorts[{i1}]:  {unknown13Shorts[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Has Vertex Colors:  {hasVertexColors}");
	if (hasVertexColors) {
		array_output_count = 0;
		for (var i2 = 0; i2 < vertexColors.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Vertex Colors[{i2}]:  {vertexColors[i2]}");
			array_output_count++;
		}
	}
	s.AppendLine($"  Num UV Sets:  {numUvSets}");
	s.AppendLine($"  Has UV:  {hasUv}");
	array_output_count = 0;
	for (var i1 = 0; i1 < uvSets.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		for (var i2 = 0; i2 < uvSets[i1].Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      UV Sets[{i2}]:  {uvSets[i1][i2]}");
			array_output_count++;
		}
	}
	s.AppendLine($"  Consistency Flags:  {consistencyFlags}");
	s.AppendLine($"  Additional Data:  {additionalData}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	if (info.version >= 0x14000004) {
		additionalData = FixLink<AbstractAdditionalGeometryData>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (additionalData != null)
		refs.Add((NiObject)additionalData);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}

//--BEGIN:FILE FOOT--//
        /*! The mesh vertex indices. */
        protected IList<int> vertexIndices;

        /*! The mapping between Nif & Max UV sets. */
        protected Dictionary<int, int> uvSetMap; // first = Max index, second = Nif index

        // Calculate bounding sphere using minimum-volume axis-align bounding box.  Its fast but not a very good fit.
        static void CalcAxisAlignedBox(IList<Vector3> vertices, out Vector3 center, out float radius)
        {
            //--Calculate center & radius--//
            //Set lows and highs to first vertex
            var lows = vertices[0];
            var highs = vertices[0];

            //Iterate through the vertices, adjusting the stored values
            //if a vertex with lower or higher values is found
            for (var i = 0; i < vertices.Count; ++i)
            {
                var v = vertices[i];
                if (v.x > highs.x) highs.x = v.x;
                else if (v.x < lows.x) lows.x = v.x;
                if (v.y > highs.y) highs.y = v.y;
                else if (v.y < lows.y) lows.y = v.y;
                if (v.z > highs.z) highs.z = v.z;
                else if (v.z < lows.z) lows.z = v.z;
            }

            //Now we know the extent of the shape, so the center will be the average
            //of the lows and highs
            center = (highs + lows) / 2.0f;

            //The radius will be the largest distance from the center
            Vector3 diff;
            float dist2 = 0.0f, maxdist2 = 0.0f;
            for (var i = 0; i < vertices.Count; ++i)
            {
                var v = vertices[i];
                diff = center - v;
                dist2 = diff.x * diff.x + diff.y * diff.y + diff.z * diff.z;
                if (dist2 > maxdist2) maxdist2 = dist2;
            }
            radius = (float)Math.Sqrt(maxdist2);
        }

        // Calculate bounding sphere using average position of the points.  Better fit but slower.
        static void CalcCenteredSphere(IList<Vector3> vertices, out Vector3 center, out float radius)
        {
            var nv = vertices.Count;
            var sum = new Vector3();
            for (var i = 0; i < nv; ++i)
                sum += vertices[i];
            center = sum / (float)nv;
            radius = 0.0f;
            for (var i = 0; i < nv; ++i)
            {
                var diff = vertices[i] - center;
                var mag = diff.Magnitude();
                radius = (float)Math.Max(radius, mag);
            }
        }


        //--Counts--//

        /*! 
         * Returns the number of verticies that make up this mesh.  This is also the number of normals, colors, and UV coordinates if these are used.
         * \return The number of vertices that make up this mesh.
         * \sa IShapeData::SetVertexCount
         */
        public int VertexCount => vertices.Count;

        /*! 
         * Returns the number of texture coordinate sets used by this mesh.  For each UV set, there is a pair of texture coordinates for every vertex in the mesh.  Each set corresponds to a texture entry in the NiTexturingPropery object.
         * \return The number of texture cooridnate sets used by this mesh.  Can be zero.
         * \sa IShapeData::SetUVSetCount, ITexturingProperty
         */
        public short VSetCount => (short)uvSets.Count;

        /*! 
         * Changes the number of UV sets used by this mesh.  If the new size is smaller, data at the end of the array will be lost.  Otherwise it will be retained.  The number of UV sets must correspond with the number of textures defined in the corresponding NiTexturingProperty object.
         * \param n The new size of the uv set array.
         * \sa IShapeData::GetUVSetCount, ITexturingProperty
         */
        public void SetUVSetCount(int n)
        {
            uvSets.Resize(n);
            hasUv = uvSets.Count != 0;
            for (var i = 0; i < uvSets.Count; ++i)
                uvSets[i].Resize(vertices.Count);
        }

        /*! 
         * Returns the number of vertec indices that make up this mesh.
         * \return The number of vertex indices that make up this mesh.
         * \sa IShapeData::SetVertexIndexCount
         */
        public int VertexIndexCount => vertexIndices.Count;

        //--Getters--//

        /*! 
         * Returns the 3D center of the mesh.
         * \return The center of this mesh.
         */
        public Vector3 Center => center;

        /*! 
         * Returns the radius of the mesh.  That is the distance from the center to
         * the farthest point from the center.
         * \return The radius of this mesh.
         */
        public float Radius => radius;

        /*! 
         * Assigns the center and radius of the spherical bound of this data.
         * \remark GeoMorpher controllers will alter the model bound.
         */
        public void SetBound(Vector3 center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }

        /*! 
         * Used to retrive the vertices used by this mesh.  The size of the vector will be the same as the vertex count retrieved with the IShapeData::GetVertexCount function.
         * \return A vector cntaining the vertices used by this mesh.
         * \sa IShapeData::SetVertices, IShapeData::GetVertexCount, IShapeData::SetVertexCount.
         */
        public IList<Vector3> Vertices => vertices;

        /*! 
         * Used to retrive the normals used by this mesh.  The size of the vector will either be zero if no normals are used, or be the same as the vertex count retrieved with the IShapeData::GetVertexCount function.
         * \return A vector cntaining the normals used by this mesh, if any.
         * \sa IShapeData::SetNormals, IShapeData::GetVertexCount, IShapeData::SetVertexCount.
         */
        public IList<Vector3> Normals => normals;

        /*! 
         * Used to retrive the vertex colors used by this mesh.  The size of the vector will either be zero if no vertex colors are used, or be the same as the vertex count retrieved with the IShapeData::GetVertexCount function.
         * \return A vector cntaining the vertex colors used by this mesh, if any.
         * \sa IShapeData::SetVertexColors, IShapeData::GetVertexCount, IShapeData::SetVertexCount.
         */
        public IList<Color4> Colors => vertexColors;

        /*! 
         * Used to retrive the texture coordinates from one of the texture sets used by this mesh.  The function will throw an exception if a texture set index that does not exist is specified.  The size of the vector will be the same as the vertex count retrieved with the IShapeData::GetVertexCount function.
         * \param index The index of the texture coordinate set to retrieve the texture coordinates from.  This index is zero based and must be a positive number smaller than that returned by the IShapeData::GetUVSetCount function.  If there are no texture coordinate sets, this function will throw an exception.
         * \return A vector cntaining the the texture coordinates used by the requested texture coordinate set.
         * \sa IShapeData::SetUVSet, IShapeData::GetUVSetCount, IShapeData::SetUVSetCount, IShapeData::GetVertexCount, IShapeData::SetVertexCount.
         */
        public IList<TexCoord> GetUVSet(int index) => uvSets[index];

        /*! 
         * Used to retrive the vertex indices used by this mesh.  The size of the vector will be the same as the vertex count retrieved with the IShapeData::GetVertexIndexCount function.
         * \return A vector containing the vertex indices used by this mesh.
         * \sa IShapeData::SetVertexIndices, IShapeData::GetVertexIndexCount, IShapeData::SetVertexIndexCount.
         */
        public IList<int> VertexIndices => vertexIndices;

        /*! 
         * Used to retrive the the NIF index corresponding to the Max map channel. If there isn't one, -1 is returned.
         * \param maxMapChannel The max map channel of the desired UV set.
         * \return A int representing the NIF index of the UV se used.
         */
        public int GetUVSetIndex(int maxMapChannel)
        {
            if (uvSetMap.Count == 0) return -1;
            if (!uvSetMap.TryGetValue(maxMapChannel, out var val)) return -1;
            return val;
        }

        //--Setters--//

        /*! 
         * Used to set the vertex data used by this mesh.  Calling this function will clear all other data in this object.
         * \param in A vector containing the vertices to replace those in the mesh with.  Note that there is no way to set vertices one at a time, they must be sent in one batch.
         * \sa IShapeData::GetVertices, IShapeData::GetVertexCount
         */
        public virtual void SetVertices(IList<Vector3> value)
        {
            vertices = value;
            hasVertices = vertices.Count != 0;

            //Clear out all other data as it is now based on old vertex information
            normals.Clear();
            hasNormals = false;
            vertexColors.Clear();
            hasVertexColors = false;
            for (var i = 0; i < uvSets.Count; ++i)
                uvSets[i].Clear();

            //If any vertices were given, calculate the new center and radius
            //Check if there are no vertices
            if (vertices.Count == 0)
            {
                center.Set(0.0f, 0.0f, 0.0f);
                radius = 0.0f;
                return;
            }

            //Set lows and highs to first vertex
            var lows = vertices[0];
            var highs = vertices[0];

            //Iterate through the rest of the vertices, adjusting the stored values
            //if a vertex with lower or higher values is found
            for (var idx = 1; idx < vertices.Count; idx++)
            {
                var i = vertices[idx];
                if (i.x > highs.x) highs.x = i.x;
                else if (i.x < lows.x) lows.x = i.x;
                if (i.y > highs.y) highs.y = i.y;
                else if (i.y < lows.y) lows.y = i.y;
                if (i.z > highs.z) highs.z = i.z;
                else if (i.z < lows.z) lows.z = i.z;
            }

            //Now we know the extent of the shape, so the center will be the average of the lows and highs.
            center.x = (highs.x + lows.x) / 2.0f;
            center.y = (highs.y + lows.y) / 2.0f;
            center.z = (highs.z + lows.z) / 2.0f;

            //The radius will be the largest distance from the center
            Vector3 diff;
            float dist2 = 0.0f, maxdist2 = 0.0f;
            foreach (var i in vertices)
            {
                diff = center;
                diff.x -= i.x;
                diff.y -= i.y;
                diff.z -= i.z;
                dist2 = diff.x * diff.x + diff.y * diff.y + diff.z * diff.z;
                if (dist2 > maxdist2) maxdist2 = dist2;
            }
            radius = (float)Math.Sqrt(maxdist2);
        }

        /*!
         * Used to set the normal data used by this mesh.  The size of the vector must either be zero, or the same as the vertex count retrieved with the IShapeData::GetVertexCount function or the function will throw an exception.
         * \param in A vector containing the normals to replace those in the mesh with.  Note that there is no way to set normals one at a time, they must be sent in one batch.  Use an empty vector to signify that this mesh will not be using normals.
         * \sa IShapeData::GetNormals, IShapeData::GetVertexCount, IShapeData::SetVertexCount.
         */
        public void SetNormals(IList<Vector3> value)
        {
            if (value.Count != vertices.Count && value.Count != 0)
                throw new Exception("Vector size must equal Vertex Count or zero.");
            normals = value;
            hasNormals = normals.Count != 0;
        }

        /*!
         * Used to set the vertex color data used by this mesh.  The size of the vector must either be zero, or the same as the vertex count retrieved with the IShapeData::GetVertexCount function or the function will throw an exception.
         * \param in A vector containing the vertex colors to replace those in the mesh with.  Note that there is no way to set vertex colors one at a time, they must be sent in one batch.  Use an empty vector to signify that this mesh will not be using vertex colors.
         * \sa IShapeData::GetColors, IShapeData::GetVertexCount, IShapeData::SetVertexCount.
         */
        public void SetVertexColors(IList<Color4> value)
        {
            if (value.Count != vertices.Count && value.Count != 0)
                throw new Exception("Vector size must equal Vertex Count or zero.");
            vertexColors = value;
            hasVertexColors = vertexColors.Count != 0;
        }

        /*!
         * Used to set the texture coordinate data from one of the texture sets used by this mesh.  The function will throw an exception if a texture set index that does not exist is specified.  The size of the vector must be the same as the vertex count retrieved with the IShapeData::GetVertexCount function, or the function will throw an exception.
         * \param index The index of the texture coordinate set to retrieve the texture coordinates from.  This index is zero based and must be a positive number smaller than that returned by the IShapeData::GetUVSetCount function.  If there are no texture coordinate sets, this function will throw an exception.
         * \param in A vector containing the the new texture coordinates to replace those in the requested texture coordinate set.
         * \sa IShapeData::GetUVSet, IShapeData::GetUVSetCount, IShapeData::SetUVSetCount, IShapeData::GetVertexCount, IShapeData::SetVertexCount.
         */
        public void SetUVSet(int index, IList<TexCoord> value)
        {
            if (value.Count != vertices.Count)
                throw new Exception("Vector size must equal Vertex Count.");
            uvSets[index] = value;
        }

        /*! 
         * Used to set the vertex index data used by this mesh.  Calling this function will clear all other data in this object.
         * \param in A vector containing the vertex indices to replace those in the mesh with.  Note that there is no way to set vertices one at a time, they must be sent in one batch.
         * \sa IShapeData::GetVertexIndices, IShapeData::GetVertexIndexCount
         */
        public virtual void SetVertexIndices(IList<int> value)
        {
            if (value.Count != vertices.Count && value.Count != 0)
                throw new Exception("Vector size must equal Vertex Count or zero.");
            vertexIndices = value;
        }

        /*! 
         * Used to set the UV set mapping data used by this mesh.  This info maps the Max map channel to the index used in the NIF.
         * \param in A map of UV set indices; first is the Max map channel and the second is the index used in the Nif mesh.
         */
        public virtual void SetUVSetMap(Dictionary<int, int> value) => uvSetMap = value;

        /*!
         * Used to apply a transformation directly to all the vertices and normals in
         * this mesh.
         * \param[in] transform The 4x4 transformation matrix to apply to the vertices and normals in this mesh.  Normals are only affected by the rotation portion of this matrix.
         */
        public void Transform(Matrix44 transform)
        {
            var rotation = new Matrix44(transform.GetRotation());
            //Apply the transformations
            for (var i = 0; i < vertices.Count; ++i)
                vertices[i] = transform * vertices[i];
            for (var i = 0; i < normals.Count; ++i)
                normals[i] = rotation * normals[i];
            CalcAxisAlignedBox(vertices, out center, out radius);
        }

        // Consistency Flags
        // \return The current value.
        public ConsistencyType ConsistencyFlags
        {
            get => consistencyFlags;
            set => consistencyFlags = value;
        }

        // Methods for saving bitangents and tangents saved in upper byte.
        // \param[in] value The new value.
        public byte TspaceFlag
        {
            get => (numUvSets | bsNumUvSets) >> 8;
            set
            {
                numUvSets = (ushort)((value << 8) | numUvSets);
                bsNumUvSets = (ushort)((value << 8) | bsNumUvSets);
            }
        }

        // Do we have lighting normals? These are essential for proper lighting: if not
        // present, the model will only be influenced by ambient light.
        // \param[in] value The new value.
        public bool HasNormals
        {
            get => hasNormals;
            set => hasNormals = value;
        }

        // Unknown. Binormal & tangents? has_normals must be set as well for this field to
        // be present.
        // \param[in] value The new value.
        public IList<Vector3> Bitangents
        {
            get => bitangents;
            set => bitangents = value;
        }


        // Unknown. Binormal & tangents?
        // \param[in] value The new value.
        public IList<Vector3> Tangents
        {
            get => tangents;
            set => tangents = value;
        }

        public SkyrimHavokMaterial SkyrimMaterial => skyrimMaterial;

        ushort numUvSetsCalc(NifInfo info) => (numUvSets & (~63)) | (ushort)(uvSets.Count & 63);
        ushort bsNumUvSetsCalc(NifInfo info) => (numUvSets & (~1)) | (bsNumUvSets & (~1)) | (ushort)(uvSets.Count & 1);
//--END:CUSTOM--//

}

}