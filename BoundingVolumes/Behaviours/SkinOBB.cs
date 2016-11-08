﻿using UnityEngine;
using System.Collections;
using Recon.BoundingVolumes;

namespace Recon.BoundingVolumes.Behaviour {
    
    public class SkinOBB : ConvexBuilder {
        public Color color = Color.white;

        ConvexUpdator _convUp;
        SkinnedMeshRenderer _attachedSkinmesh;
        OBB _obb;

        protected void OnDrawGizmos() {
			if (!isActiveAndEnabled || _obb == null)
                return;

            ConvUp.AssureUpdateConvex ();
            Gizmos.color = color;
            _obb.DrawGizmos ();

        }

        public ConvexUpdator ConvUp {
            get { return (_convUp == null ? (_convUp = new ConvexUpdator (this)) : _convUp); }
        }

        #region Skinned Mesh
        public SkinnedMeshRenderer AttachedSkinMesh {
            get { return _attachedSkinmesh; }
        }
        #endregion
        #region implemented abstract members of IConvex
        public override IConvexPolyhedron GetConvexPolyhedron () {
            ConvUp.AssureUpdateConvex ();
            return _obb;
        }
        public override bool StartConvex () {
            return (_attachedSkinmesh == null ? 
                (_attachedSkinmesh = GetComponentInChildren<SkinnedMeshRenderer> ()) != null : true);
        }
        public override bool UpdateConvex () {
            return (_obb = OBB.Create (_attachedSkinmesh.rootBone, _attachedSkinmesh.localBounds)) != null;
        }
        #endregion
    }
}
