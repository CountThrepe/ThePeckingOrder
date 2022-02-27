using UnityEngine;
using Cinemachine;

/// <summary>
/// An add-on module for Cinemachine Virtual Camera that locks the camera's Z co-ordinate
/// </summary>
[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class MaxX : CinemachineExtension {
    [Tooltip("Confine X to max value")]
    public float xMax = 220;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime) {

        if (enabled && stage == CinemachineCore.Stage.Body) {
            var pos = state.RawPosition;
            if (pos.x > xMax) {
                pos.x = xMax;
                state.RawPosition = pos;
            }
        }
    }
}