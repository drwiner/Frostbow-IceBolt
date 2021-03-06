﻿using Cinemachine.Timeline;
using ClipNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TimelineClipsNamespace;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ClipNamespace
{
    public class TrackManager
    {
        // A manager for a track type that only adds new tracks when there is a conflict
        //public List<TrackAsset> Tracks;
        public TrackAsset currentTrack;
        public List<TimelineClip> TimelineClips = new List<TimelineClip>();
        private int trackNum = 0;
        private string name;

        private TimelineAsset timeline;

        public TrackManager(TimelineAsset _timeline, string trackName)
        {
            name = trackName;
            timeline = _timeline;
            currentTrack = timeline.CreateTrack<ControlTrack> (null, name + trackNum.ToString());
        }

        public virtual TimelineClip CreateClip(float start, float duration, string displayName)
        {
            TimelineClip tc;

            if (FreeInterval(start, start + duration, TimelineClips))
            {
                tc = currentTrack.CreateDefaultClip();
            }
            else
            {
                trackNum++;
                currentTrack = timeline.CreateTrack<ControlTrack>(null, name + trackNum.ToString());
                TimelineClips = new List<TimelineClip>();
                tc = currentTrack.CreateDefaultClip();
            }

            tc.start = start;
            tc.duration = duration;
            tc.displayName = displayName;

            TimelineClips.Add(tc);
            return tc;
        }

        public static bool FreeInterval(float start, float end, List<TimelineClip> TCs)
        {
            foreach (var clip in TCs)
            {
                if (clip.start >= start && clip.start <= end)
                {
                    return false;
                }
                if (clip.end <= end && clip.end > start)
                {
                    return false;
                }
                if (clip.start <= start && clip.end >= end)
                {
                    return false;
                }

            }
            return true;
        }
    }

    public class PlayableTrackManager
    {

        // A manager for a track type that only adds new tracks when there is a conflict
        //public List<TrackAsset> Tracks;
        public TrackAsset currentTrack;
        public List<TimelineClip> TimelineClips = new List<TimelineClip>();
        private int trackNum = 0;
        private string name;

        private TimelineAsset timeline;

        public PlayableTrackManager(TimelineAsset _timeline, string trackName)
        {
            name = trackName;
            timeline = _timeline;
            currentTrack = timeline.CreateTrack<PlayableTrack>(null, name + trackNum.ToString());
        }

        public virtual TimelineClip CreateClip(float start, float duration, string displayName)
        {
            TimelineClip tc;

            if (!FreeInterval(start, start + duration, TimelineClips))
            {
                trackNum++;
                currentTrack = timeline.CreateTrack<PlayableTrack>(null, name + trackNum.ToString());
                TimelineClips = new List<TimelineClip>();
            }

            tc = currentTrack.CreateClip<LerpMoveObjectAsset>();

            tc.start = start;
            tc.duration = duration;
            tc.displayName = displayName;

            TimelineClips.Add(tc);
            return tc;
        }

        public static bool FreeInterval(float start, float end, List<TimelineClip> TCs)
        {
            foreach (var clip in TCs)
            {
                if (clip.start >= start && clip.start <= end)
                {
                    return false;
                }
                if (clip.end <= end && clip.end > start)
                {
                    return false;
                }
                if (clip.start <= start && clip.end >= end)
                {
                    return false;
                }

            }
            return true;
        }
    }

    public class SteerTrackManager
    {

        // A manager for a track type that only adds new tracks when there is a conflict
        //public List<TrackAsset> Tracks;
        public TrackAsset currentTrack;
        public List<TimelineClip> TimelineClips = new List<TimelineClip>();
        private int trackNum = 0;
        private string name;

        private TimelineAsset timeline;

        public SteerTrackManager(TimelineAsset _timeline, string trackName)
        {
            name = trackName;
            timeline = _timeline;
            currentTrack = timeline.CreateTrack<PlayableTrack>(null, name + trackNum.ToString());
        }

        public virtual TimelineClip CreateClip(float start, float duration, string displayName)
        {
            TimelineClip tc;

            if (!FreeInterval(start, start + duration, TimelineClips))
            {
                trackNum++;
                currentTrack = timeline.CreateTrack<PlayableTrack>(null, name + trackNum.ToString());
                TimelineClips = new List<TimelineClip>();
            }

            tc = currentTrack.CreateClip<SteeringAsset>();

            tc.start = start;
            tc.duration = duration;
            tc.displayName = displayName;

            TimelineClips.Add(tc);
            return tc;
        }

        public static bool FreeInterval(float start, float end, List<TimelineClip> TCs)
        {
            foreach (var clip in TCs)
            {
                if (clip.start >= start && clip.start <= end)
                {
                    return false;
                }
                if (clip.end <= end && clip.end > start)
                {
                    return false;
                }
                if (clip.start <= start && clip.end >= end)
                {
                    return false;
                }

            }
            return true;
        }
    }

    public class CinemachineTrackManager
    // A Track Manager that doesn't allow a second track (no overlaps
    {
        public TrackAsset currentTrack;
        public List<TimelineClip> TimelineClips = new List<TimelineClip>();
        private TimelineAsset timeline;

        public CinemachineTrackManager(TimelineAsset _timeline)
        {
            timeline = _timeline;
            currentTrack = timeline.CreateTrack<CinemachineTrack>(null, "film_track");
        }

        public TimelineClip CreateClip(float start, float duration, string displayName)
        {
            TimelineClip tc;
            tc = currentTrack.CreateDefaultClip();

            tc.start = start + .015f;// .06f;
            tc.duration = duration;
            tc.displayName = displayName;


            TimelineClips.Add(tc);
            return tc;
        }
    }

}