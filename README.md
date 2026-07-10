A Meta Quest hand-tracking interaction where the user pinches/grips a tap handle to rotate it, controlling water flow into a sink basin. Built in Unity with XR Interaction Toolkit + OpenXR Hand Tracking.

**Video**
https://youtu.be/bdY0EZ-dcQQ

**Build**
https://1drv.ms/f/c/b02d6c0b699fa31d/IgDp-R5rL5e1S6HcaHYQyZyrAbYSXslvpY0hpvipK6KwOmU?e=h3uES1

**How It Works**

**The Tap**
The handle is an XR Grab Interactable constrained by a Hinge Joint, limited to a 0°–90° rotation range.
Grabbing it requires a pinch (hand tracking) 
The handle only counts as "open" past a small activation threshold, rather than reacting to the slightest touch this stops accidental brushes from triggering flow and makes the interaction feel closer to actually turning a real tap.

**Water Level Logic**
This is the core system most worth explaining, since it's not a real fluid simulation:

Angle → normalized flow. The tap's current rotation (read from the Hinge Joint) is normalized to a 0–1 range based on the 0°–90° limits. This single value drives everything downstream: particle emission rate, stream thickness, audio volume/pitch, and basin fill rate.

Water surface, not fluid physics. The "water" in the basin is a flat plane that starts hidden/at basin-floor height and rises on the Y-axis as the fill level increases, driven by the accumulated flow over time. It stops exactly at the basin rim height

Delay before flow starts. There's a short configurable delay between the tap crossing its activation angle and the water actually starting to account for the realistm of the real-world lag between opening a tap and water arriving. This same delay is shared by the audio fade-in, so sound and visuals feel synchronized rather than both snapping on instantly.

Ripples only appear once there's water. The ripple particle system on the water surface is dependant on the particle water system. 

Audio is asymmetric on purpose. Sound fades in gradually (matching the delayed water start) but cuts off immediately the instant the tap closes, an abrupt stop reads as more natural than a matching fade-out, since that's closer to how a real tap sounds.


**Tunable values**
These are all exposed in the Inspector rather than hardcoded, since a lot of the "hyper-realistic" feel came from iterating on numbers rather than logic:

1.Tap activation angle
2.Water fill start delay
3.Ripple/particle activation timing
4.Audio fade-in duration
5.Maximum water height (basin rim)


**Testing Without a Headset**
No Meta Quest on hand — everything below was verified using Unity's XR Device Simulator in the Editor, driven by mouse/keyboard, per the fallback allowed in the brief.

**A known limitation, and how it was handled**
While setting this up, it became clear the XR Device Simulator's hand-pinch emulation is unreliable in-editor, this is a known limitation of the tool, not specific to this project (I checked and found multiple reports of the same issue exist in Unity's own community forums, even in Unity's official sample scenes). At first this looked like a broken interaction setup (no hover, no grab response at all), which took some ruling-out (colliders, interaction layers, missing Interaction Manager references) before landing on the actual cause: the Simulator is built primarily around controller-style input, and its synthetic hand/pinch data doesn't always register cleanly

**Water Realism**
I didnt plan to make the water itself fully photorealistic, it's a simple animated surface plus particles rather than a proper fluid/shader-based sim. Given the brief centers on the interaction mechanics (pinch → rotate → flow → fill, all correctly mapped and responsive), the priority was making sure that pipeline is solid and feels physically sensible, rather than sinking time into shader-level water realism that wasn't really what was being tested. Happy to push further and learn more on the visual side with the team, but the mechanics were treated as the actual deliverable here.

To keep moving without losing test coverage, the Simulator was used in controller mode to validate the full interaction chain including grab detection, hinge rotation, angle-to-flow mapping, basin fill, audio, ripples.


Once hit play, move the handle in the scene, it should rotate smoothly within its 0°–90° limit, water should begin after the short delay, the stream should thicken as the angle increases, and the basin should visibly fill and stop at the rim.


**Extra Notes**
The most important thing that binds it all together is reading the Hinge Joint's live angle each frame, which allows through scripts, to control the audio, particle size etc. Through trial and error, it became the most reliable way to stay in sync with physics-driven rotation 

Overall fun project. Shouldn't have wasted so much time trying to figure out hand tracking with the simulator. Hope to learn more about realisitc water simulation.
