# Unity Google Streaming Speech-to-Text

## Compatibility

This library has been tested on OS X and Windows 10 in Unity 2019.1.4f1. At this time, I don't have the time and resources to add support for Android, iOS, or other platforms, but I welcome PRs that address this.

## Installation

This plugin is available as a Unity package hosted on [NPM](https://www.npmjs.com/package/com.oshoham.unity-google-cloud-streaming-speech-to-text). To install it, add the following lines to your Unity project's `Packages/manifest.json`:

```json
{
  "scopedRegistries": [
    {
      "name": "npm",
      "url": "https://registry.npmjs.com",
      "scopes": [
        "com.oshoham"
      ]
    }
  ],
  "dependencies": {
    // other dependencies go here
    "com.oshoham.unity-google-cloud-streaming-speech-to-text": "0.1.8"
  }
}
```

## Setup

1. In the Project Settings menu, change Player -> Configuration -> API Compatibility Level to **.NET 4.x**. 
2. Follow step 1 of Google's [Cloud Speech-to-Text Quickstart Guide](https://cloud.google.com/speech-to-text/docs/quickstart-client-libraries#before-you-begin) to:
    1. Set up a GCP Console project.
    2. Enable the Speech-to-Text API for your project.
    3. Create a service account.
    4. Download your service account's private key as a JSON file.
3. Rename your private key JSON file to `gcp_credentials.json`.
4. Place your `gcp_credentials.json` file in a folder called `Assets/StreamingAssets` in your Unity project.

## Usage

In your Unity scene, create a new `GameObject` and attach the `StreamingRecognizer` MonoBehavior to it.

If you want to quickly test that things are working, check the `Enable Debug Logging` option on the `StreamingRecognizer`, then play your scene. You should see some debugging messages appear in the Console, along with a live transcription of any speech audible to your computer's microphone.
