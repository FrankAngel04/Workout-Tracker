; ModuleID = 'marshal_methods.armeabi-v7a.ll'
source_filename = "marshal_methods.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [317 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [628 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 68
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 67
	i32 15721112, ; 2: System.Runtime.Intrinsics.dll => 0xefe298 => 108
	i32 32687329, ; 3: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 237
	i32 34715100, ; 4: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 271
	i32 34839235, ; 5: System.IO.FileSystem.DriveInfo => 0x2139ac3 => 48
	i32 39485524, ; 6: System.Net.WebSockets.dll => 0x25a8054 => 80
	i32 42639949, ; 7: System.Threading.Thread => 0x28aa24d => 145
	i32 53857724, ; 8: ca/Microsoft.Maui.Controls.resources => 0x335cdbc => 280
	i32 66541672, ; 9: System.Diagnostics.StackTrace => 0x3f75868 => 30
	i32 68219467, ; 10: System.Security.Cryptography.Primitives => 0x410f24b => 124
	i32 72070932, ; 11: Microsoft.Maui.Graphics.dll => 0x44bb714 => 190
	i32 82292897, ; 12: System.Runtime.CompilerServices.VisualC.dll => 0x4e7b0a1 => 102
	i32 101534019, ; 13: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 255
	i32 113429830, ; 14: zh-HK/Microsoft.Maui.Controls.resources => 0x6c2cd46 => 310
	i32 117431740, ; 15: System.Runtime.InteropServices => 0x6ffddbc => 107
	i32 120558881, ; 16: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 255
	i32 122350210, ; 17: System.Threading.Channels.dll => 0x74aea82 => 139
	i32 134690465, ; 18: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 275
	i32 142721839, ; 19: System.Net.WebHeaderCollection => 0x881c32f => 77
	i32 149972175, ; 20: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 124
	i32 159306688, ; 21: System.ComponentModel.Annotations => 0x97ed3c0 => 13
	i32 165246403, ; 22: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 211
	i32 176265551, ; 23: System.ServiceProcess => 0xa81994f => 132
	i32 182336117, ; 24: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 257
	i32 184328833, ; 25: System.ValueTuple.dll => 0xafca281 => 151
	i32 195452805, ; 26: vi/Microsoft.Maui.Controls.resources.dll => 0xba65f85 => 309
	i32 199333315, ; 27: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xbe195c3 => 310
	i32 205061960, ; 28: System.ComponentModel => 0xc38ff48 => 18
	i32 209399409, ; 29: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 209
	i32 220171995, ; 30: System.Diagnostics.Debug => 0xd1f8edb => 26
	i32 230216969, ; 31: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 231
	i32 230752869, ; 32: Microsoft.CSharp.dll => 0xdc10265 => 1
	i32 231409092, ; 33: System.Linq.Parallel => 0xdcb05c4 => 59
	i32 231814094, ; 34: System.Globalization => 0xdd133ce => 42
	i32 246610117, ; 35: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 91
	i32 261689757, ; 36: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 214
	i32 276479776, ; 37: System.Threading.Timer.dll => 0x107abf20 => 147
	i32 278686392, ; 38: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 233
	i32 280482487, ; 39: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 230
	i32 280992041, ; 40: cs/Microsoft.Maui.Controls.resources.dll => 0x10bf9929 => 281
	i32 291076382, ; 41: System.IO.Pipes.AccessControl.dll => 0x1159791e => 54
	i32 298918909, ; 42: System.Net.Ping.dll => 0x11d123fd => 69
	i32 318968648, ; 43: Xamarin.AndroidX.Activity.dll => 0x13031348 => 200
	i32 321597661, ; 44: System.Numerics => 0x132b30dd => 83
	i32 336156722, ; 45: ja/Microsoft.Maui.Controls.resources.dll => 0x14095832 => 294
	i32 342366114, ; 46: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 232
	i32 347068432, ; 47: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0x14afd810 => 194
	i32 356389973, ; 48: it/Microsoft.Maui.Controls.resources.dll => 0x153e1455 => 293
	i32 357576608, ; 49: cs/Microsoft.Maui.Controls.resources => 0x15502fa0 => 281
	i32 360082299, ; 50: System.ServiceModel.Web => 0x15766b7b => 131
	i32 367780167, ; 51: System.IO.Pipes => 0x15ebe147 => 55
	i32 374914964, ; 52: System.Transactions.Local => 0x1658bf94 => 149
	i32 375677976, ; 53: System.Net.ServicePoint.dll => 0x16646418 => 74
	i32 379916513, ; 54: System.Threading.Thread.dll => 0x16a510e1 => 145
	i32 385762202, ; 55: System.Memory.dll => 0x16fe439a => 62
	i32 392610295, ; 56: System.Threading.ThreadPool.dll => 0x1766c1f7 => 146
	i32 395744057, ; 57: _Microsoft.Android.Resource.Designer => 0x17969339 => 313
	i32 403441872, ; 58: WindowsBase => 0x180c08d0 => 165
	i32 435591531, ; 59: sv/Microsoft.Maui.Controls.resources.dll => 0x19f6996b => 305
	i32 441335492, ; 60: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 215
	i32 442565967, ; 61: System.Collections => 0x1a61054f => 12
	i32 450948140, ; 62: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 228
	i32 451504562, ; 63: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 125
	i32 456227837, ; 64: System.Web.HttpUtility.dll => 0x1b317bfd => 152
	i32 459347974, ; 65: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 113
	i32 465846621, ; 66: mscorlib => 0x1bc4415d => 166
	i32 469710990, ; 67: System.dll => 0x1bff388e => 164
	i32 476646585, ; 68: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 230
	i32 486930444, ; 69: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 243
	i32 498788369, ; 70: System.ObjectModel => 0x1dbae811 => 84
	i32 500358224, ; 71: id/Microsoft.Maui.Controls.resources.dll => 0x1dd2dc50 => 292
	i32 503918385, ; 72: fi/Microsoft.Maui.Controls.resources.dll => 0x1e092f31 => 286
	i32 513247710, ; 73: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 184
	i32 526420162, ; 74: System.Transactions.dll => 0x1f6088c2 => 150
	i32 527168573, ; 75: hi/Microsoft.Maui.Controls.resources => 0x1f6bf43d => 289
	i32 527452488, ; 76: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 275
	i32 530272170, ; 77: System.Linq.Queryable => 0x1f9b4faa => 60
	i32 539058512, ; 78: Microsoft.Extensions.Logging => 0x20216150 => 180
	i32 540030774, ; 79: System.IO.FileSystem.dll => 0x20303736 => 51
	i32 545304856, ; 80: System.Runtime.Extensions => 0x2080b118 => 103
	i32 546455878, ; 81: System.Runtime.Serialization.Xml => 0x20924146 => 114
	i32 549171840, ; 82: System.Globalization.Calendars => 0x20bbb280 => 40
	i32 557405415, ; 83: Jsr305Binding => 0x213954e7 => 268
	i32 569601784, ; 84: Xamarin.AndroidX.Window.Extensions.Core.Core => 0x21f36ef8 => 266
	i32 577335427, ; 85: System.Security.Cryptography.Cng => 0x22697083 => 120
	i32 592146354, ; 86: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x234b6fb2 => 300
	i32 601371474, ; 87: System.IO.IsolatedStorage.dll => 0x23d83352 => 52
	i32 605376203, ; 88: System.IO.Compression.FileSystem => 0x24154ecb => 44
	i32 613668793, ; 89: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 119
	i32 627609679, ; 90: Xamarin.AndroidX.CustomView => 0x2568904f => 220
	i32 639843206, ; 91: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 226
	i32 643868501, ; 92: System.Net => 0x2660a755 => 81
	i32 662205335, ; 93: System.Text.Encodings.Web.dll => 0x27787397 => 136
	i32 663517072, ; 94: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 262
	i32 666292255, ; 95: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 207
	i32 672442732, ; 96: System.Collections.Concurrent => 0x2814a96c => 8
	i32 683518922, ; 97: System.Net.Security => 0x28bdabca => 73
	i32 688181140, ; 98: ca/Microsoft.Maui.Controls.resources.dll => 0x2904cf94 => 280
	i32 690569205, ; 99: System.Xml.Linq.dll => 0x29293ff5 => 155
	i32 691348768, ; 100: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 277
	i32 693804605, ; 101: System.Windows => 0x295a9e3d => 154
	i32 699345723, ; 102: System.Reflection.Emit => 0x29af2b3b => 92
	i32 700284507, ; 103: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 272
	i32 700358131, ; 104: System.IO.Compression.ZipFile => 0x29be9df3 => 45
	i32 706645707, ; 105: ko/Microsoft.Maui.Controls.resources.dll => 0x2a1e8ecb => 295
	i32 709557578, ; 106: de/Microsoft.Maui.Controls.resources.dll => 0x2a4afd4a => 283
	i32 720511267, ; 107: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 276
	i32 722857257, ; 108: System.Runtime.Loader.dll => 0x2b15ed29 => 109
	i32 735137430, ; 109: System.Security.SecureString.dll => 0x2bd14e96 => 129
	i32 748832960, ; 110: SQLitePCLRaw.batteries_v2 => 0x2ca248c0 => 192
	i32 752232764, ; 111: System.Diagnostics.Contracts.dll => 0x2cd6293c => 25
	i32 755313932, ; 112: Xamarin.Android.Glide.Annotations.dll => 0x2d052d0c => 197
	i32 759454413, ; 113: System.Net.Requests => 0x2d445acd => 72
	i32 762598435, ; 114: System.IO.Pipes.dll => 0x2d745423 => 55
	i32 775507847, ; 115: System.IO.Compression => 0x2e394f87 => 46
	i32 789151979, ; 116: Microsoft.Extensions.Options => 0x2f0980eb => 183
	i32 790371945, ; 117: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 0x2f1c1e69 => 221
	i32 804715423, ; 118: System.Data.Common => 0x2ff6fb9f => 22
	i32 807930345, ; 119: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 0x302809e9 => 235
	i32 823281589, ; 120: System.Private.Uri.dll => 0x311247b5 => 86
	i32 830298997, ; 121: System.IO.Compression.Brotli => 0x317d5b75 => 43
	i32 832635846, ; 122: System.Xml.XPath.dll => 0x31a103c6 => 160
	i32 834051424, ; 123: System.Net.Quic => 0x31b69d60 => 71
	i32 843511501, ; 124: Xamarin.AndroidX.Print => 0x3246f6cd => 248
	i32 870878177, ; 125: ar/Microsoft.Maui.Controls.resources => 0x33e88be1 => 279
	i32 873119928, ; 126: Microsoft.VisualBasic => 0x340ac0b8 => 3
	i32 877678880, ; 127: System.Globalization.dll => 0x34505120 => 42
	i32 878954865, ; 128: System.Net.Http.Json => 0x3463c971 => 63
	i32 904024072, ; 129: System.ComponentModel.Primitives.dll => 0x35e25008 => 16
	i32 911108515, ; 130: System.IO.MemoryMappedFiles.dll => 0x364e69a3 => 53
	i32 926902833, ; 131: tr/Microsoft.Maui.Controls.resources.dll => 0x373f6a31 => 307
	i32 928116545, ; 132: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 271
	i32 952186615, ; 133: System.Runtime.InteropServices.JavaScript.dll => 0x38c136f7 => 105
	i32 956575887, ; 134: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 276
	i32 966729478, ; 135: Xamarin.Google.Crypto.Tink.Android => 0x399f1f06 => 269
	i32 967690846, ; 136: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 232
	i32 975236339, ; 137: System.Diagnostics.Tracing => 0x3a20ecf3 => 34
	i32 975874589, ; 138: System.Xml.XDocument => 0x3a2aaa1d => 158
	i32 986514023, ; 139: System.Private.DataContractSerialization.dll => 0x3acd0267 => 85
	i32 987214855, ; 140: System.Diagnostics.Tools => 0x3ad7b407 => 32
	i32 992768348, ; 141: System.Collections.dll => 0x3b2c715c => 12
	i32 993161700, ; 142: zh-Hans/Microsoft.Maui.Controls.resources => 0x3b3271e4 => 311
	i32 994442037, ; 143: System.IO.FileSystem => 0x3b45fb35 => 51
	i32 994547685, ; 144: es/Microsoft.Maui.Controls.resources => 0x3b4797e5 => 285
	i32 1001831731, ; 145: System.IO.UnmanagedMemoryStream.dll => 0x3bb6bd33 => 56
	i32 1012816738, ; 146: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 252
	i32 1019214401, ; 147: System.Drawing => 0x3cbffa41 => 36
	i32 1023149569, ; 148: WorkoutTrackerApp.dll => 0x3cfc0601 => 0
	i32 1028951442, ; 149: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 179
	i32 1029334545, ; 150: da/Microsoft.Maui.Controls.resources.dll => 0x3d5a6611 => 282
	i32 1031528504, ; 151: Xamarin.Google.ErrorProne.Annotations.dll => 0x3d7be038 => 270
	i32 1035644815, ; 152: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 205
	i32 1036536393, ; 153: System.Drawing.Primitives.dll => 0x3dc84a49 => 35
	i32 1044663988, ; 154: System.Linq.Expressions.dll => 0x3e444eb4 => 58
	i32 1052210849, ; 155: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 239
	i32 1067306892, ; 156: GoogleGson => 0x3f9dcf8c => 175
	i32 1082857460, ; 157: System.ComponentModel.TypeConverter => 0x408b17f4 => 17
	i32 1084122840, ; 158: Xamarin.Kotlin.StdLib => 0x409e66d8 => 273
	i32 1098259244, ; 159: System => 0x41761b2c => 164
	i32 1121599056, ; 160: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 0x42da3e50 => 238
	i32 1127624469, ; 161: Microsoft.Extensions.Logging.Debug => 0x43362f15 => 182
	i32 1149092582, ; 162: Xamarin.AndroidX.Window => 0x447dc2e6 => 265
	i32 1170634674, ; 163: System.Web.dll => 0x45c677b2 => 153
	i32 1175144683, ; 164: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 261
	i32 1178241025, ; 165: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 246
	i32 1178797549, ; 166: fi/Microsoft.Maui.Controls.resources => 0x464305ed => 286
	i32 1203215381, ; 167: pl/Microsoft.Maui.Controls.resources.dll => 0x47b79c15 => 299
	i32 1204270330, ; 168: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 207
	i32 1208641965, ; 169: System.Diagnostics.Process => 0x480a69ad => 29
	i32 1214827643, ; 170: CommunityToolkit.Mvvm => 0x4868cc7b => 174
	i32 1219128291, ; 171: System.IO.IsolatedStorage => 0x48aa6be3 => 52
	i32 1234928153, ; 172: nb/Microsoft.Maui.Controls.resources.dll => 0x499b8219 => 297
	i32 1243150071, ; 173: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 0x4a18f6f7 => 266
	i32 1253011324, ; 174: Microsoft.Win32.Registry => 0x4aaf6f7c => 5
	i32 1264511973, ; 175: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 256
	i32 1267360935, ; 176: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 260
	i32 1273260888, ; 177: Xamarin.AndroidX.Collection.Ktx => 0x4be46b58 => 212
	i32 1275534314, ; 178: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 277
	i32 1278448581, ; 179: Xamarin.AndroidX.Annotation.Jvm => 0x4c3393c5 => 204
	i32 1292207520, ; 180: SQLitePCLRaw.core.dll => 0x4d0585a0 => 193
	i32 1293217323, ; 181: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 223
	i32 1309188875, ; 182: System.Private.DataContractSerialization => 0x4e08a30b => 85
	i32 1322716291, ; 183: Xamarin.AndroidX.Window.dll => 0x4ed70c83 => 265
	i32 1324164729, ; 184: System.Linq => 0x4eed2679 => 61
	i32 1335329327, ; 185: System.Runtime.Serialization.Json.dll => 0x4f97822f => 112
	i32 1364015309, ; 186: System.IO => 0x514d38cd => 57
	i32 1376866003, ; 187: Xamarin.AndroidX.SavedState => 0x52114ed3 => 252
	i32 1379779777, ; 188: System.Resources.ResourceManager => 0x523dc4c1 => 99
	i32 1402170036, ; 189: System.Configuration.dll => 0x53936ab4 => 19
	i32 1406073936, ; 190: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 216
	i32 1408764838, ; 191: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 111
	i32 1411638395, ; 192: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 101
	i32 1422545099, ; 193: System.Runtime.CompilerServices.VisualC => 0x54ca50cb => 102
	i32 1434145427, ; 194: System.Runtime.Handles => 0x557b5293 => 104
	i32 1435222561, ; 195: Xamarin.Google.Crypto.Tink.Android.dll => 0x558bc221 => 269
	i32 1439761251, ; 196: System.Net.Quic.dll => 0x55d10363 => 71
	i32 1452070440, ; 197: System.Formats.Asn1.dll => 0x568cd628 => 38
	i32 1453312822, ; 198: System.Diagnostics.Tools.dll => 0x569fcb36 => 32
	i32 1457743152, ; 199: System.Runtime.Extensions.dll => 0x56e36530 => 103
	i32 1458022317, ; 200: System.Net.Security.dll => 0x56e7a7ad => 73
	i32 1461234159, ; 201: System.Collections.Immutable.dll => 0x5718a9ef => 9
	i32 1461719063, ; 202: System.Security.Cryptography.OpenSsl => 0x57201017 => 123
	i32 1462112819, ; 203: System.IO.Compression.dll => 0x57261233 => 46
	i32 1469204771, ; 204: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 206
	i32 1470490898, ; 205: Microsoft.Extensions.Primitives => 0x57a5e912 => 184
	i32 1479771757, ; 206: System.Collections.Immutable => 0x5833866d => 9
	i32 1480492111, ; 207: System.IO.Compression.Brotli.dll => 0x583e844f => 43
	i32 1487239319, ; 208: Microsoft.Win32.Primitives => 0x58a57897 => 4
	i32 1490025113, ; 209: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 0x58cffa99 => 253
	i32 1493001747, ; 210: hi/Microsoft.Maui.Controls.resources.dll => 0x58fd6613 => 289
	i32 1514721132, ; 211: el/Microsoft.Maui.Controls.resources.dll => 0x5a48cf6c => 284
	i32 1536373174, ; 212: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 31
	i32 1543031311, ; 213: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 138
	i32 1543355203, ; 214: System.Reflection.Emit.dll => 0x5bfdbb43 => 92
	i32 1550322496, ; 215: System.Reflection.Extensions.dll => 0x5c680b40 => 93
	i32 1551623176, ; 216: sk/Microsoft.Maui.Controls.resources.dll => 0x5c7be408 => 304
	i32 1554762148, ; 217: fr/Microsoft.Maui.Controls.resources => 0x5cabc9a4 => 287
	i32 1565862583, ; 218: System.IO.FileSystem.Primitives => 0x5d552ab7 => 49
	i32 1566207040, ; 219: System.Threading.Tasks.Dataflow.dll => 0x5d5a6c40 => 141
	i32 1573704789, ; 220: System.Runtime.Serialization.Json => 0x5dccd455 => 112
	i32 1580037396, ; 221: System.Threading.Overlapped => 0x5e2d7514 => 140
	i32 1580413037, ; 222: sv/Microsoft.Maui.Controls.resources => 0x5e33306d => 305
	i32 1582372066, ; 223: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 222
	i32 1591080825, ; 224: zh-Hant/Microsoft.Maui.Controls.resources => 0x5ed5f779 => 312
	i32 1592978981, ; 225: System.Runtime.Serialization.dll => 0x5ef2ee25 => 115
	i32 1597949149, ; 226: Xamarin.Google.ErrorProne.Annotations => 0x5f3ec4dd => 270
	i32 1601112923, ; 227: System.Xml.Serialization => 0x5f6f0b5b => 157
	i32 1604827217, ; 228: System.Net.WebClient => 0x5fa7b851 => 76
	i32 1618516317, ; 229: System.Net.WebSockets.Client.dll => 0x6078995d => 79
	i32 1622152042, ; 230: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 242
	i32 1622358360, ; 231: System.Dynamic.Runtime => 0x60b33958 => 37
	i32 1624863272, ; 232: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 264
	i32 1635184631, ; 233: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 226
	i32 1636350590, ; 234: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 219
	i32 1639515021, ; 235: System.Net.Http.dll => 0x61b9038d => 64
	i32 1639986890, ; 236: System.Text.RegularExpressions => 0x61c036ca => 138
	i32 1641389582, ; 237: System.ComponentModel.EventBasedAsync.dll => 0x61d59e0e => 15
	i32 1657153582, ; 238: System.Runtime => 0x62c6282e => 116
	i32 1658241508, ; 239: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 258
	i32 1658251792, ; 240: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 267
	i32 1670060433, ; 241: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 214
	i32 1675553242, ; 242: System.IO.FileSystem.DriveInfo.dll => 0x63dee9da => 48
	i32 1677501392, ; 243: System.Net.Primitives.dll => 0x63fca3d0 => 70
	i32 1678508291, ; 244: System.Net.WebSockets => 0x640c0103 => 80
	i32 1679769178, ; 245: System.Security.Cryptography => 0x641f3e5a => 126
	i32 1691477237, ; 246: System.Reflection.Metadata => 0x64d1e4f5 => 94
	i32 1696967625, ; 247: System.Security.Cryptography.Csp => 0x6525abc9 => 121
	i32 1698840827, ; 248: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 274
	i32 1701541528, ; 249: System.Diagnostics.Debug.dll => 0x656b7698 => 26
	i32 1711441057, ; 250: SQLitePCLRaw.lib.e_sqlite3.android => 0x660284a1 => 194
	i32 1720223769, ; 251: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 0x66888819 => 235
	i32 1726116996, ; 252: System.Reflection.dll => 0x66e27484 => 97
	i32 1728033016, ; 253: System.Diagnostics.FileVersionInfo.dll => 0x66ffb0f8 => 28
	i32 1729485958, ; 254: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 210
	i32 1736233607, ; 255: ro/Microsoft.Maui.Controls.resources.dll => 0x677cd287 => 302
	i32 1744735666, ; 256: System.Transactions.Local.dll => 0x67fe8db2 => 149
	i32 1746316138, ; 257: Mono.Android.Export => 0x6816ab6a => 169
	i32 1750313021, ; 258: Microsoft.Win32.Primitives.dll => 0x6853a83d => 4
	i32 1758240030, ; 259: System.Resources.Reader.dll => 0x68cc9d1e => 98
	i32 1763938596, ; 260: System.Diagnostics.TraceSource.dll => 0x69239124 => 33
	i32 1765942094, ; 261: System.Reflection.Extensions => 0x6942234e => 93
	i32 1766324549, ; 262: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 257
	i32 1770582343, ; 263: Microsoft.Extensions.Logging.dll => 0x6988f147 => 180
	i32 1776026572, ; 264: System.Core.dll => 0x69dc03cc => 21
	i32 1777075843, ; 265: System.Globalization.Extensions.dll => 0x69ec0683 => 41
	i32 1780572499, ; 266: Mono.Android.Runtime.dll => 0x6a216153 => 170
	i32 1788241197, ; 267: Xamarin.AndroidX.Fragment => 0x6a96652d => 228
	i32 1808609942, ; 268: Xamarin.AndroidX.Loader => 0x6bcd3296 => 242
	i32 1809966115, ; 269: nb/Microsoft.Maui.Controls.resources => 0x6be1e423 => 297
	i32 1813058853, ; 270: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 273
	i32 1813201214, ; 271: Xamarin.Google.Android.Material => 0x6c13413e => 267
	i32 1818569960, ; 272: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 247
	i32 1818787751, ; 273: Microsoft.VisualBasic.Core => 0x6c687fa7 => 2
	i32 1821794637, ; 274: hu/Microsoft.Maui.Controls.resources => 0x6c96614d => 291
	i32 1824175904, ; 275: System.Text.Encoding.Extensions => 0x6cbab720 => 134
	i32 1824722060, ; 276: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 111
	i32 1828688058, ; 277: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 181
	i32 1842015223, ; 278: uk/Microsoft.Maui.Controls.resources.dll => 0x6dcaebf7 => 308
	i32 1847515442, ; 279: Xamarin.Android.Glide.Annotations => 0x6e1ed932 => 197
	i32 1858542181, ; 280: System.Linq.Expressions => 0x6ec71a65 => 58
	i32 1870277092, ; 281: System.Reflection.Primitives => 0x6f7a29e4 => 95
	i32 1879696579, ; 282: System.Formats.Tar.dll => 0x7009e4c3 => 39
	i32 1885316902, ; 283: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 208
	i32 1888955245, ; 284: System.Diagnostics.Contracts => 0x70972b6d => 25
	i32 1889954781, ; 285: System.Reflection.Metadata.dll => 0x70a66bdd => 94
	i32 1898237753, ; 286: System.Reflection.DispatchProxy => 0x7124cf39 => 89
	i32 1900610850, ; 287: System.Resources.ResourceManager.dll => 0x71490522 => 99
	i32 1910275211, ; 288: System.Collections.NonGeneric.dll => 0x71dc7c8b => 10
	i32 1939592360, ; 289: System.Private.Xml.Linq => 0x739bd4a8 => 87
	i32 1956758971, ; 290: System.Resources.Writer => 0x74a1c5bb => 100
	i32 1960264639, ; 291: ja/Microsoft.Maui.Controls.resources => 0x74d743bf => 294
	i32 1961813231, ; 292: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x74eee4ef => 254
	i32 1968388702, ; 293: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 176
	i32 1983156543, ; 294: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 274
	i32 1985761444, ; 295: Xamarin.Android.Glide.GifDecoder => 0x765c50a4 => 199
	i32 2011961780, ; 296: System.Buffers.dll => 0x77ec19b4 => 7
	i32 2014344398, ; 297: hr/Microsoft.Maui.Controls.resources => 0x781074ce => 290
	i32 2019465201, ; 298: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 239
	i32 2025202353, ; 299: ar/Microsoft.Maui.Controls.resources.dll => 0x78b622b1 => 279
	i32 2031763787, ; 300: Xamarin.Android.Glide => 0x791a414b => 196
	i32 2043674646, ; 301: it/Microsoft.Maui.Controls.resources => 0x79d00016 => 293
	i32 2045470958, ; 302: System.Private.Xml => 0x79eb68ee => 88
	i32 2055257422, ; 303: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 234
	i32 2060060697, ; 304: System.Windows.dll => 0x7aca0819 => 154
	i32 2070888862, ; 305: System.Diagnostics.TraceSource => 0x7b6f419e => 33
	i32 2079903147, ; 306: System.Runtime.dll => 0x7bf8cdab => 116
	i32 2090596640, ; 307: System.Numerics.Vectors => 0x7c9bf920 => 82
	i32 2103459038, ; 308: SQLitePCLRaw.provider.e_sqlite3.dll => 0x7d603cde => 195
	i32 2127167465, ; 309: System.Console => 0x7ec9ffe9 => 20
	i32 2142473426, ; 310: System.Collections.Specialized => 0x7fb38cd2 => 11
	i32 2143790110, ; 311: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 162
	i32 2146852085, ; 312: Microsoft.VisualBasic.dll => 0x7ff65cf5 => 3
	i32 2150663486, ; 313: ko/Microsoft.Maui.Controls.resources => 0x8030853e => 295
	i32 2159891885, ; 314: Microsoft.Maui => 0x80bd55ad => 188
	i32 2165051842, ; 315: ro/Microsoft.Maui.Controls.resources => 0x810c11c2 => 302
	i32 2181898931, ; 316: Microsoft.Extensions.Options.dll => 0x820d22b3 => 183
	i32 2192057212, ; 317: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 181
	i32 2193016926, ; 318: System.ObjectModel.dll => 0x82b6c85e => 84
	i32 2201107256, ; 319: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 278
	i32 2201231467, ; 320: System.Net.Http => 0x8334206b => 64
	i32 2217644978, ; 321: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 261
	i32 2222056684, ; 322: System.Threading.Tasks.Parallel => 0x8471e4ec => 143
	i32 2244775296, ; 323: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 243
	i32 2252106437, ; 324: System.Xml.Serialization.dll => 0x863c6ac5 => 157
	i32 2256313426, ; 325: System.Globalization.Extensions => 0x867c9c52 => 41
	i32 2265110946, ; 326: System.Security.AccessControl.dll => 0x8702d9a2 => 117
	i32 2266799131, ; 327: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 177
	i32 2267999099, ; 328: Xamarin.Android.Glide.DiskLruCache.dll => 0x872eeb7b => 198
	i32 2270573516, ; 329: fr/Microsoft.Maui.Controls.resources.dll => 0x875633cc => 287
	i32 2279755925, ; 330: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 250
	i32 2289298199, ; 331: th/Microsoft.Maui.Controls.resources => 0x8873eb17 => 306
	i32 2293034957, ; 332: System.ServiceModel.Web.dll => 0x88acefcd => 131
	i32 2295906218, ; 333: System.Net.Sockets => 0x88d8bfaa => 75
	i32 2298471582, ; 334: System.Net.Mail => 0x88ffe49e => 66
	i32 2305521784, ; 335: System.Private.CoreLib.dll => 0x896b7878 => 172
	i32 2315684594, ; 336: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 202
	i32 2320631194, ; 337: System.Threading.Tasks.Parallel.dll => 0x8a52059a => 143
	i32 2340441535, ; 338: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 106
	i32 2344264397, ; 339: System.ValueTuple => 0x8bbaa2cd => 151
	i32 2353062107, ; 340: System.Net.Primitives => 0x8c40e0db => 70
	i32 2368005991, ; 341: System.Xml.ReaderWriter.dll => 0x8d24e767 => 156
	i32 2369760409, ; 342: tr/Microsoft.Maui.Controls.resources => 0x8d3fac99 => 307
	i32 2371007202, ; 343: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 176
	i32 2378619854, ; 344: System.Security.Cryptography.Csp.dll => 0x8dc6dbce => 121
	i32 2383496789, ; 345: System.Security.Principal.Windows.dll => 0x8e114655 => 127
	i32 2401565422, ; 346: System.Web.HttpUtility => 0x8f24faee => 152
	i32 2403452196, ; 347: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 225
	i32 2421380589, ; 348: System.Threading.Tasks.Dataflow => 0x905355ed => 141
	i32 2421992093, ; 349: nl/Microsoft.Maui.Controls.resources => 0x905caa9d => 298
	i32 2423080555, ; 350: Xamarin.AndroidX.Collection.Ktx.dll => 0x906d466b => 212
	i32 2435356389, ; 351: System.Console.dll => 0x912896e5 => 20
	i32 2435904999, ; 352: System.ComponentModel.DataAnnotations.dll => 0x9130f5e7 => 14
	i32 2454642406, ; 353: System.Text.Encoding.dll => 0x924edee6 => 135
	i32 2458678730, ; 354: System.Net.Sockets.dll => 0x928c75ca => 75
	i32 2459001652, ; 355: System.Linq.Parallel.dll => 0x92916334 => 59
	i32 2465273461, ; 356: SQLitePCLRaw.batteries_v2.dll => 0x92f11675 => 192
	i32 2465532216, ; 357: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 215
	i32 2471841756, ; 358: netstandard.dll => 0x93554fdc => 167
	i32 2475788418, ; 359: Java.Interop.dll => 0x93918882 => 168
	i32 2480646305, ; 360: Microsoft.Maui.Controls => 0x93dba8a1 => 186
	i32 2483903535, ; 361: System.ComponentModel.EventBasedAsync => 0x940d5c2f => 15
	i32 2484371297, ; 362: System.Net.ServicePoint => 0x94147f61 => 74
	i32 2490993605, ; 363: System.AppContext.dll => 0x94798bc5 => 6
	i32 2501346920, ; 364: System.Data.DataSetExtensions => 0x95178668 => 23
	i32 2505896520, ; 365: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 237
	i32 2520433370, ; 366: sk/Microsoft.Maui.Controls.resources => 0x963ac2da => 304
	i32 2522472828, ; 367: Xamarin.Android.Glide.dll => 0x9659e17c => 196
	i32 2538310050, ; 368: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 91
	i32 2562349572, ; 369: Microsoft.CSharp => 0x98ba5a04 => 1
	i32 2570120770, ; 370: System.Text.Encodings.Web => 0x9930ee42 => 136
	i32 2581783588, ; 371: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 0x99e2e424 => 238
	i32 2581819634, ; 372: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 260
	i32 2585220780, ; 373: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 134
	i32 2585805581, ; 374: System.Net.Ping => 0x9a20430d => 69
	i32 2589602615, ; 375: System.Threading.ThreadPool => 0x9a5a3337 => 146
	i32 2605712449, ; 376: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 278
	i32 2615233544, ; 377: Xamarin.AndroidX.Fragment.Ktx => 0x9be14c08 => 229
	i32 2616218305, ; 378: Microsoft.Extensions.Logging.Debug.dll => 0x9bf052c1 => 182
	i32 2617129537, ; 379: System.Private.Xml.dll => 0x9bfe3a41 => 88
	i32 2618712057, ; 380: System.Reflection.TypeExtensions.dll => 0x9c165ff9 => 96
	i32 2620871830, ; 381: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 219
	i32 2624644809, ; 382: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 224
	i32 2627185994, ; 383: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 31
	i32 2629843544, ; 384: System.IO.Compression.ZipFile.dll => 0x9cc03a58 => 45
	i32 2633051222, ; 385: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 233
	i32 2663391936, ; 386: Xamarin.Android.Glide.DiskLruCache => 0x9ec022c0 => 198
	i32 2663698177, ; 387: System.Runtime.Loader => 0x9ec4cf01 => 109
	i32 2664396074, ; 388: System.Xml.XDocument.dll => 0x9ecf752a => 158
	i32 2665622720, ; 389: System.Drawing.Primitives => 0x9ee22cc0 => 35
	i32 2676780864, ; 390: System.Data.Common.dll => 0x9f8c6f40 => 22
	i32 2686887180, ; 391: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 114
	i32 2693849962, ; 392: System.IO.dll => 0xa090e36a => 57
	i32 2701096212, ; 393: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 258
	i32 2715334215, ; 394: System.Threading.Tasks.dll => 0xa1d8b647 => 144
	i32 2717744543, ; 395: System.Security.Claims => 0xa1fd7d9f => 118
	i32 2719963679, ; 396: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 120
	i32 2724373263, ; 397: System.Runtime.Numerics.dll => 0xa262a30f => 110
	i32 2732626843, ; 398: Xamarin.AndroidX.Activity => 0xa2e0939b => 200
	i32 2735172069, ; 399: System.Threading.Channels => 0xa30769e5 => 139
	i32 2737747696, ; 400: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 206
	i32 2740948882, ; 401: System.IO.Pipes.AccessControl => 0xa35f8f92 => 54
	i32 2748088231, ; 402: System.Runtime.InteropServices.JavaScript => 0xa3cc7fa7 => 105
	i32 2758225723, ; 403: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 187
	i32 2764765095, ; 404: Microsoft.Maui.dll => 0xa4caf7a7 => 188
	i32 2765824710, ; 405: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 133
	i32 2770495804, ; 406: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 272
	i32 2778768386, ; 407: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 263
	i32 2779977773, ; 408: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 251
	i32 2788224221, ; 409: Xamarin.AndroidX.Fragment.Ktx.dll => 0xa630ecdd => 229
	i32 2801831435, ; 410: Microsoft.Maui.Graphics => 0xa7008e0b => 190
	i32 2802068195, ; 411: uk/Microsoft.Maui.Controls.resources => 0xa7042ae3 => 308
	i32 2803228030, ; 412: System.Xml.XPath.XDocument.dll => 0xa715dd7e => 159
	i32 2806116107, ; 413: es/Microsoft.Maui.Controls.resources.dll => 0xa741ef0b => 285
	i32 2810250172, ; 414: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 216
	i32 2819470561, ; 415: System.Xml.dll => 0xa80db4e1 => 163
	i32 2821205001, ; 416: System.ServiceProcess.dll => 0xa8282c09 => 132
	i32 2821294376, ; 417: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 251
	i32 2824502124, ; 418: System.Xml.XmlDocument => 0xa85a7b6c => 161
	i32 2831556043, ; 419: nl/Microsoft.Maui.Controls.resources.dll => 0xa8c61dcb => 298
	i32 2838993487, ; 420: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 0xa9379a4f => 240
	i32 2849599387, ; 421: System.Threading.Overlapped.dll => 0xa9d96f9b => 140
	i32 2853208004, ; 422: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 263
	i32 2855708567, ; 423: Xamarin.AndroidX.Transition => 0xaa36a797 => 259
	i32 2857259519, ; 424: el/Microsoft.Maui.Controls.resources => 0xaa4e51ff => 284
	i32 2861098320, ; 425: Mono.Android.Export.dll => 0xaa88e550 => 169
	i32 2861189240, ; 426: Microsoft.Maui.Essentials => 0xaa8a4878 => 189
	i32 2870099610, ; 427: Xamarin.AndroidX.Activity.Ktx.dll => 0xab123e9a => 201
	i32 2875164099, ; 428: Jsr305Binding.dll => 0xab5f85c3 => 268
	i32 2875220617, ; 429: System.Globalization.Calendars.dll => 0xab606289 => 40
	i32 2883495834, ; 430: ru/Microsoft.Maui.Controls.resources => 0xabdea79a => 303
	i32 2884993177, ; 431: Xamarin.AndroidX.ExifInterface => 0xabf58099 => 227
	i32 2887636118, ; 432: System.Net.dll => 0xac1dd496 => 81
	i32 2899753641, ; 433: System.IO.UnmanagedMemoryStream => 0xacd6baa9 => 56
	i32 2900621748, ; 434: System.Dynamic.Runtime.dll => 0xace3f9b4 => 37
	i32 2901442782, ; 435: System.Reflection => 0xacf080de => 97
	i32 2905242038, ; 436: mscorlib.dll => 0xad2a79b6 => 166
	i32 2909740682, ; 437: System.Private.CoreLib => 0xad6f1e8a => 172
	i32 2916838712, ; 438: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 264
	i32 2919462931, ; 439: System.Numerics.Vectors.dll => 0xae037813 => 82
	i32 2921128767, ; 440: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 203
	i32 2936416060, ; 441: System.Resources.Reader => 0xaf06273c => 98
	i32 2940926066, ; 442: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 30
	i32 2942453041, ; 443: System.Xml.XPath.XDocument => 0xaf624531 => 159
	i32 2959614098, ; 444: System.ComponentModel.dll => 0xb0682092 => 18
	i32 2968338931, ; 445: System.Security.Principal.Windows => 0xb0ed41f3 => 127
	i32 2970759306, ; 446: BCrypt.Net-Next.dll => 0xb112308a => 173
	i32 2972252294, ; 447: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 119
	i32 2978675010, ; 448: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 223
	i32 2987532451, ; 449: Xamarin.AndroidX.Security.SecurityCrypto => 0xb21220a3 => 254
	i32 2996846495, ; 450: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 236
	i32 3016983068, ; 451: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 256
	i32 3023353419, ; 452: WindowsBase.dll => 0xb434b64b => 165
	i32 3024354802, ; 453: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 231
	i32 3038032645, ; 454: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 313
	i32 3056245963, ; 455: Xamarin.AndroidX.SavedState.SavedState.Ktx => 0xb62a9ccb => 253
	i32 3057625584, ; 456: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 244
	i32 3059408633, ; 457: Mono.Android.Runtime => 0xb65adef9 => 170
	i32 3059793426, ; 458: System.ComponentModel.Primitives => 0xb660be12 => 16
	i32 3075834255, ; 459: System.Threading.Tasks => 0xb755818f => 144
	i32 3077302341, ; 460: hu/Microsoft.Maui.Controls.resources.dll => 0xb76be845 => 291
	i32 3090735792, ; 461: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 125
	i32 3099732863, ; 462: System.Security.Claims.dll => 0xb8c22b7f => 118
	i32 3103600923, ; 463: System.Formats.Asn1 => 0xb8fd311b => 38
	i32 3111772706, ; 464: System.Runtime.Serialization => 0xb979e222 => 115
	i32 3121463068, ; 465: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 47
	i32 3124832203, ; 466: System.Threading.Tasks.Extensions => 0xba4127cb => 142
	i32 3132293585, ; 467: System.Security.AccessControl => 0xbab301d1 => 117
	i32 3147165239, ; 468: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 34
	i32 3148237826, ; 469: GoogleGson.dll => 0xbba64c02 => 175
	i32 3159123045, ; 470: System.Reflection.Primitives.dll => 0xbc4c6465 => 95
	i32 3160747431, ; 471: System.IO.MemoryMappedFiles => 0xbc652da7 => 53
	i32 3178803400, ; 472: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 245
	i32 3192346100, ; 473: System.Security.SecureString => 0xbe4755f4 => 129
	i32 3193515020, ; 474: System.Web => 0xbe592c0c => 153
	i32 3204380047, ; 475: System.Data.dll => 0xbefef58f => 24
	i32 3209718065, ; 476: System.Xml.XmlDocument.dll => 0xbf506931 => 161
	i32 3211777861, ; 477: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 222
	i32 3220365878, ; 478: System.Threading => 0xbff2e236 => 148
	i32 3226221578, ; 479: System.Runtime.Handles.dll => 0xc04c3c0a => 104
	i32 3251039220, ; 480: System.Reflection.DispatchProxy.dll => 0xc1c6ebf4 => 89
	i32 3258312781, ; 481: Xamarin.AndroidX.CardView => 0xc235e84d => 210
	i32 3265493905, ; 482: System.Linq.Queryable.dll => 0xc2a37b91 => 60
	i32 3265893370, ; 483: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 142
	i32 3277815716, ; 484: System.Resources.Writer.dll => 0xc35f7fa4 => 100
	i32 3279906254, ; 485: Microsoft.Win32.Registry.dll => 0xc37f65ce => 5
	i32 3280506390, ; 486: System.ComponentModel.Annotations.dll => 0xc3888e16 => 13
	i32 3286872994, ; 487: SQLite-net.dll => 0xc3e9b3a2 => 191
	i32 3290767353, ; 488: System.Security.Cryptography.Encoding => 0xc4251ff9 => 122
	i32 3299363146, ; 489: System.Text.Encoding => 0xc4a8494a => 135
	i32 3303498502, ; 490: System.Diagnostics.FileVersionInfo => 0xc4e76306 => 28
	i32 3316684772, ; 491: System.Net.Requests.dll => 0xc5b097e4 => 72
	i32 3317135071, ; 492: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 220
	i32 3317144872, ; 493: System.Data => 0xc5b79d28 => 24
	i32 3340431453, ; 494: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 208
	i32 3345895724, ; 495: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 249
	i32 3346324047, ; 496: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 246
	i32 3358260929, ; 497: System.Text.Json => 0xc82afec1 => 137
	i32 3360279109, ; 498: SQLitePCLRaw.core => 0xc849ca45 => 193
	i32 3362336904, ; 499: Xamarin.AndroidX.Activity.Ktx => 0xc8693088 => 201
	i32 3362522851, ; 500: Xamarin.AndroidX.Core => 0xc86c06e3 => 217
	i32 3366347497, ; 501: Java.Interop => 0xc8a662e9 => 168
	i32 3374999561, ; 502: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 250
	i32 3395150330, ; 503: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 101
	i32 3403906625, ; 504: System.Security.Cryptography.OpenSsl.dll => 0xcae37e41 => 123
	i32 3405233483, ; 505: Xamarin.AndroidX.CustomView.PoolingContainer => 0xcaf7bd4b => 221
	i32 3428513518, ; 506: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 178
	i32 3429136800, ; 507: System.Xml => 0xcc6479a0 => 163
	i32 3430777524, ; 508: netstandard => 0xcc7d82b4 => 167
	i32 3441283291, ; 509: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 224
	i32 3445260447, ; 510: System.Formats.Tar => 0xcd5a809f => 39
	i32 3452344032, ; 511: Microsoft.Maui.Controls.Compatibility.dll => 0xcdc696e0 => 185
	i32 3463511458, ; 512: hr/Microsoft.Maui.Controls.resources.dll => 0xce70fda2 => 290
	i32 3471940407, ; 513: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 17
	i32 3472012038, ; 514: BCrypt.Net-Next => 0xcef2b306 => 173
	i32 3476120550, ; 515: Mono.Android => 0xcf3163e6 => 171
	i32 3479583265, ; 516: ru/Microsoft.Maui.Controls.resources.dll => 0xcf663a21 => 303
	i32 3485117614, ; 517: System.Text.Json.dll => 0xcfbaacae => 137
	i32 3486566296, ; 518: System.Transactions => 0xcfd0c798 => 150
	i32 3493954962, ; 519: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 213
	i32 3509114376, ; 520: System.Xml.Linq => 0xd128d608 => 155
	i32 3515174580, ; 521: System.Security.dll => 0xd1854eb4 => 130
	i32 3530912306, ; 522: System.Configuration => 0xd2757232 => 19
	i32 3539954161, ; 523: System.Net.HttpListener => 0xd2ff69f1 => 65
	i32 3542658132, ; 524: vi/Microsoft.Maui.Controls.resources => 0xd328ac54 => 309
	i32 3560100363, ; 525: System.Threading.Timer => 0xd432d20b => 147
	i32 3570554715, ; 526: System.IO.FileSystem.AccessControl => 0xd4d2575b => 47
	i32 3596930546, ; 527: de/Microsoft.Maui.Controls.resources => 0xd664cdf2 => 283
	i32 3597029428, ; 528: Xamarin.Android.Glide.GifDecoder.dll => 0xd6665034 => 199
	i32 3598340787, ; 529: System.Net.WebSockets.Client => 0xd67a52b3 => 79
	i32 3608519521, ; 530: System.Linq.dll => 0xd715a361 => 61
	i32 3623444314, ; 531: da/Microsoft.Maui.Controls.resources => 0xd7f95f5a => 282
	i32 3624195450, ; 532: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 106
	i32 3627220390, ; 533: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 248
	i32 3633644679, ; 534: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 203
	i32 3636224170, ; 535: WorkoutTrackerApp => 0xd8bc60aa => 0
	i32 3638274909, ; 536: System.IO.FileSystem.Primitives.dll => 0xd8dbab5d => 49
	i32 3641597786, ; 537: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 234
	i32 3643854240, ; 538: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 245
	i32 3645089577, ; 539: System.ComponentModel.DataAnnotations => 0xd943a729 => 14
	i32 3647796983, ; 540: pt-BR/Microsoft.Maui.Controls.resources => 0xd96cf6f7 => 300
	i32 3657292374, ; 541: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 177
	i32 3660523487, ; 542: System.Net.NetworkInformation => 0xda2f27df => 68
	i32 3662115805, ; 543: he/Microsoft.Maui.Controls.resources => 0xda4773dd => 288
	i32 3672681054, ; 544: Mono.Android.dll => 0xdae8aa5e => 171
	i32 3682565725, ; 545: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 209
	i32 3684561358, ; 546: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 213
	i32 3686075795, ; 547: ms/Microsoft.Maui.Controls.resources => 0xdbb50d93 => 296
	i32 3697841164, ; 548: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xdc68940c => 312
	i32 3700866549, ; 549: System.Net.WebProxy.dll => 0xdc96bdf5 => 78
	i32 3706696989, ; 550: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 218
	i32 3716563718, ; 551: System.Runtime.Intrinsics => 0xdd864306 => 108
	i32 3718780102, ; 552: Xamarin.AndroidX.Annotation => 0xdda814c6 => 202
	i32 3724971120, ; 553: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 244
	i32 3732100267, ; 554: System.Net.NameResolution => 0xde7354ab => 67
	i32 3737834244, ; 555: System.Net.Http.Json.dll => 0xdecad304 => 63
	i32 3748608112, ; 556: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 27
	i32 3751444290, ; 557: System.Xml.XPath => 0xdf9a7f42 => 160
	i32 3754567612, ; 558: SQLitePCLRaw.provider.e_sqlite3 => 0xdfca27bc => 195
	i32 3786282454, ; 559: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 211
	i32 3792276235, ; 560: System.Collections.NonGeneric => 0xe2098b0b => 10
	i32 3800979733, ; 561: Microsoft.Maui.Controls.Compatibility => 0xe28e5915 => 185
	i32 3802395368, ; 562: System.Collections.Specialized.dll => 0xe2a3f2e8 => 11
	i32 3819260425, ; 563: System.Net.WebProxy => 0xe3a54a09 => 78
	i32 3823082795, ; 564: System.Security.Cryptography.dll => 0xe3df9d2b => 126
	i32 3829621856, ; 565: System.Numerics.dll => 0xe4436460 => 83
	i32 3841636137, ; 566: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 179
	i32 3844307129, ; 567: System.Net.Mail.dll => 0xe52378b9 => 66
	i32 3849253459, ; 568: System.Runtime.InteropServices.dll => 0xe56ef253 => 107
	i32 3870376305, ; 569: System.Net.HttpListener.dll => 0xe6b14171 => 65
	i32 3873536506, ; 570: System.Security.Principal => 0xe6e179fa => 128
	i32 3875112723, ; 571: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 122
	i32 3876362041, ; 572: SQLite-net => 0xe70c9739 => 191
	i32 3885497537, ; 573: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 77
	i32 3885922214, ; 574: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 259
	i32 3888767677, ; 575: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 249
	i32 3889960447, ; 576: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xe7dc15ff => 311
	i32 3896106733, ; 577: System.Collections.Concurrent.dll => 0xe839deed => 8
	i32 3896760992, ; 578: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 217
	i32 3901907137, ; 579: Microsoft.VisualBasic.Core.dll => 0xe89260c1 => 2
	i32 3920810846, ; 580: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 44
	i32 3921031405, ; 581: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 262
	i32 3928044579, ; 582: System.Xml.ReaderWriter => 0xea213423 => 156
	i32 3930554604, ; 583: System.Security.Principal.dll => 0xea4780ec => 128
	i32 3931092270, ; 584: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 247
	i32 3945713374, ; 585: System.Data.DataSetExtensions.dll => 0xeb2ecede => 23
	i32 3953953790, ; 586: System.Text.Encoding.CodePages => 0xebac8bfe => 133
	i32 3955647286, ; 587: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 205
	i32 3959773229, ; 588: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 236
	i32 3980434154, ; 589: th/Microsoft.Maui.Controls.resources.dll => 0xed409aea => 306
	i32 3987592930, ; 590: he/Microsoft.Maui.Controls.resources.dll => 0xedadd6e2 => 288
	i32 4003436829, ; 591: System.Diagnostics.Process.dll => 0xee9f991d => 29
	i32 4015948917, ; 592: Xamarin.AndroidX.Annotation.Jvm.dll => 0xef5e8475 => 204
	i32 4025784931, ; 593: System.Memory => 0xeff49a63 => 62
	i32 4046471985, ; 594: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 187
	i32 4054681211, ; 595: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 90
	i32 4068434129, ; 596: System.Private.Xml.Linq.dll => 0xf27f60d1 => 87
	i32 4070331268, ; 597: id/Microsoft.Maui.Controls.resources => 0xf29c5384 => 292
	i32 4073602200, ; 598: System.Threading.dll => 0xf2ce3c98 => 148
	i32 4094352644, ; 599: Microsoft.Maui.Essentials.dll => 0xf40add04 => 189
	i32 4099507663, ; 600: System.Drawing.dll => 0xf45985cf => 36
	i32 4100113165, ; 601: System.Private.Uri => 0xf462c30d => 86
	i32 4101593132, ; 602: Xamarin.AndroidX.Emoji2 => 0xf479582c => 225
	i32 4102112229, ; 603: pt/Microsoft.Maui.Controls.resources.dll => 0xf48143e5 => 301
	i32 4119206479, ; 604: pl/Microsoft.Maui.Controls.resources => 0xf5861a4f => 299
	i32 4125707920, ; 605: ms/Microsoft.Maui.Controls.resources.dll => 0xf5e94e90 => 296
	i32 4126470640, ; 606: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 178
	i32 4127667938, ; 607: System.IO.FileSystem.Watcher => 0xf60736e2 => 50
	i32 4130442656, ; 608: System.AppContext => 0xf6318da0 => 6
	i32 4147896353, ; 609: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 90
	i32 4151237749, ; 610: System.Core => 0xf76edc75 => 21
	i32 4159265925, ; 611: System.Xml.XmlSerializer => 0xf7e95c85 => 162
	i32 4161255271, ; 612: System.Reflection.TypeExtensions => 0xf807b767 => 96
	i32 4164802419, ; 613: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 50
	i32 4181436372, ; 614: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 113
	i32 4182413190, ; 615: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 241
	i32 4185676441, ; 616: System.Security => 0xf97c5a99 => 130
	i32 4196529839, ; 617: System.Net.WebClient.dll => 0xfa21f6af => 76
	i32 4213026141, ; 618: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 27
	i32 4234116406, ; 619: pt/Microsoft.Maui.Controls.resources => 0xfc5f7d36 => 301
	i32 4256097574, ; 620: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 218
	i32 4258378803, ; 621: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 0xfdd1b433 => 240
	i32 4260525087, ; 622: System.Buffers => 0xfdf2741f => 7
	i32 4271975918, ; 623: Microsoft.Maui.Controls.dll => 0xfea12dee => 186
	i32 4274623895, ; 624: CommunityToolkit.Mvvm.dll => 0xfec99597 => 174
	i32 4274976490, ; 625: System.Runtime.Numerics => 0xfecef6ea => 110
	i32 4292120959, ; 626: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 241
	i32 4294763496 ; 627: Xamarin.AndroidX.ExifInterface.dll => 0xfffce3e8 => 227
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [628 x i32] [
	i32 68, ; 0
	i32 67, ; 1
	i32 108, ; 2
	i32 237, ; 3
	i32 271, ; 4
	i32 48, ; 5
	i32 80, ; 6
	i32 145, ; 7
	i32 280, ; 8
	i32 30, ; 9
	i32 124, ; 10
	i32 190, ; 11
	i32 102, ; 12
	i32 255, ; 13
	i32 310, ; 14
	i32 107, ; 15
	i32 255, ; 16
	i32 139, ; 17
	i32 275, ; 18
	i32 77, ; 19
	i32 124, ; 20
	i32 13, ; 21
	i32 211, ; 22
	i32 132, ; 23
	i32 257, ; 24
	i32 151, ; 25
	i32 309, ; 26
	i32 310, ; 27
	i32 18, ; 28
	i32 209, ; 29
	i32 26, ; 30
	i32 231, ; 31
	i32 1, ; 32
	i32 59, ; 33
	i32 42, ; 34
	i32 91, ; 35
	i32 214, ; 36
	i32 147, ; 37
	i32 233, ; 38
	i32 230, ; 39
	i32 281, ; 40
	i32 54, ; 41
	i32 69, ; 42
	i32 200, ; 43
	i32 83, ; 44
	i32 294, ; 45
	i32 232, ; 46
	i32 194, ; 47
	i32 293, ; 48
	i32 281, ; 49
	i32 131, ; 50
	i32 55, ; 51
	i32 149, ; 52
	i32 74, ; 53
	i32 145, ; 54
	i32 62, ; 55
	i32 146, ; 56
	i32 313, ; 57
	i32 165, ; 58
	i32 305, ; 59
	i32 215, ; 60
	i32 12, ; 61
	i32 228, ; 62
	i32 125, ; 63
	i32 152, ; 64
	i32 113, ; 65
	i32 166, ; 66
	i32 164, ; 67
	i32 230, ; 68
	i32 243, ; 69
	i32 84, ; 70
	i32 292, ; 71
	i32 286, ; 72
	i32 184, ; 73
	i32 150, ; 74
	i32 289, ; 75
	i32 275, ; 76
	i32 60, ; 77
	i32 180, ; 78
	i32 51, ; 79
	i32 103, ; 80
	i32 114, ; 81
	i32 40, ; 82
	i32 268, ; 83
	i32 266, ; 84
	i32 120, ; 85
	i32 300, ; 86
	i32 52, ; 87
	i32 44, ; 88
	i32 119, ; 89
	i32 220, ; 90
	i32 226, ; 91
	i32 81, ; 92
	i32 136, ; 93
	i32 262, ; 94
	i32 207, ; 95
	i32 8, ; 96
	i32 73, ; 97
	i32 280, ; 98
	i32 155, ; 99
	i32 277, ; 100
	i32 154, ; 101
	i32 92, ; 102
	i32 272, ; 103
	i32 45, ; 104
	i32 295, ; 105
	i32 283, ; 106
	i32 276, ; 107
	i32 109, ; 108
	i32 129, ; 109
	i32 192, ; 110
	i32 25, ; 111
	i32 197, ; 112
	i32 72, ; 113
	i32 55, ; 114
	i32 46, ; 115
	i32 183, ; 116
	i32 221, ; 117
	i32 22, ; 118
	i32 235, ; 119
	i32 86, ; 120
	i32 43, ; 121
	i32 160, ; 122
	i32 71, ; 123
	i32 248, ; 124
	i32 279, ; 125
	i32 3, ; 126
	i32 42, ; 127
	i32 63, ; 128
	i32 16, ; 129
	i32 53, ; 130
	i32 307, ; 131
	i32 271, ; 132
	i32 105, ; 133
	i32 276, ; 134
	i32 269, ; 135
	i32 232, ; 136
	i32 34, ; 137
	i32 158, ; 138
	i32 85, ; 139
	i32 32, ; 140
	i32 12, ; 141
	i32 311, ; 142
	i32 51, ; 143
	i32 285, ; 144
	i32 56, ; 145
	i32 252, ; 146
	i32 36, ; 147
	i32 0, ; 148
	i32 179, ; 149
	i32 282, ; 150
	i32 270, ; 151
	i32 205, ; 152
	i32 35, ; 153
	i32 58, ; 154
	i32 239, ; 155
	i32 175, ; 156
	i32 17, ; 157
	i32 273, ; 158
	i32 164, ; 159
	i32 238, ; 160
	i32 182, ; 161
	i32 265, ; 162
	i32 153, ; 163
	i32 261, ; 164
	i32 246, ; 165
	i32 286, ; 166
	i32 299, ; 167
	i32 207, ; 168
	i32 29, ; 169
	i32 174, ; 170
	i32 52, ; 171
	i32 297, ; 172
	i32 266, ; 173
	i32 5, ; 174
	i32 256, ; 175
	i32 260, ; 176
	i32 212, ; 177
	i32 277, ; 178
	i32 204, ; 179
	i32 193, ; 180
	i32 223, ; 181
	i32 85, ; 182
	i32 265, ; 183
	i32 61, ; 184
	i32 112, ; 185
	i32 57, ; 186
	i32 252, ; 187
	i32 99, ; 188
	i32 19, ; 189
	i32 216, ; 190
	i32 111, ; 191
	i32 101, ; 192
	i32 102, ; 193
	i32 104, ; 194
	i32 269, ; 195
	i32 71, ; 196
	i32 38, ; 197
	i32 32, ; 198
	i32 103, ; 199
	i32 73, ; 200
	i32 9, ; 201
	i32 123, ; 202
	i32 46, ; 203
	i32 206, ; 204
	i32 184, ; 205
	i32 9, ; 206
	i32 43, ; 207
	i32 4, ; 208
	i32 253, ; 209
	i32 289, ; 210
	i32 284, ; 211
	i32 31, ; 212
	i32 138, ; 213
	i32 92, ; 214
	i32 93, ; 215
	i32 304, ; 216
	i32 287, ; 217
	i32 49, ; 218
	i32 141, ; 219
	i32 112, ; 220
	i32 140, ; 221
	i32 305, ; 222
	i32 222, ; 223
	i32 312, ; 224
	i32 115, ; 225
	i32 270, ; 226
	i32 157, ; 227
	i32 76, ; 228
	i32 79, ; 229
	i32 242, ; 230
	i32 37, ; 231
	i32 264, ; 232
	i32 226, ; 233
	i32 219, ; 234
	i32 64, ; 235
	i32 138, ; 236
	i32 15, ; 237
	i32 116, ; 238
	i32 258, ; 239
	i32 267, ; 240
	i32 214, ; 241
	i32 48, ; 242
	i32 70, ; 243
	i32 80, ; 244
	i32 126, ; 245
	i32 94, ; 246
	i32 121, ; 247
	i32 274, ; 248
	i32 26, ; 249
	i32 194, ; 250
	i32 235, ; 251
	i32 97, ; 252
	i32 28, ; 253
	i32 210, ; 254
	i32 302, ; 255
	i32 149, ; 256
	i32 169, ; 257
	i32 4, ; 258
	i32 98, ; 259
	i32 33, ; 260
	i32 93, ; 261
	i32 257, ; 262
	i32 180, ; 263
	i32 21, ; 264
	i32 41, ; 265
	i32 170, ; 266
	i32 228, ; 267
	i32 242, ; 268
	i32 297, ; 269
	i32 273, ; 270
	i32 267, ; 271
	i32 247, ; 272
	i32 2, ; 273
	i32 291, ; 274
	i32 134, ; 275
	i32 111, ; 276
	i32 181, ; 277
	i32 308, ; 278
	i32 197, ; 279
	i32 58, ; 280
	i32 95, ; 281
	i32 39, ; 282
	i32 208, ; 283
	i32 25, ; 284
	i32 94, ; 285
	i32 89, ; 286
	i32 99, ; 287
	i32 10, ; 288
	i32 87, ; 289
	i32 100, ; 290
	i32 294, ; 291
	i32 254, ; 292
	i32 176, ; 293
	i32 274, ; 294
	i32 199, ; 295
	i32 7, ; 296
	i32 290, ; 297
	i32 239, ; 298
	i32 279, ; 299
	i32 196, ; 300
	i32 293, ; 301
	i32 88, ; 302
	i32 234, ; 303
	i32 154, ; 304
	i32 33, ; 305
	i32 116, ; 306
	i32 82, ; 307
	i32 195, ; 308
	i32 20, ; 309
	i32 11, ; 310
	i32 162, ; 311
	i32 3, ; 312
	i32 295, ; 313
	i32 188, ; 314
	i32 302, ; 315
	i32 183, ; 316
	i32 181, ; 317
	i32 84, ; 318
	i32 278, ; 319
	i32 64, ; 320
	i32 261, ; 321
	i32 143, ; 322
	i32 243, ; 323
	i32 157, ; 324
	i32 41, ; 325
	i32 117, ; 326
	i32 177, ; 327
	i32 198, ; 328
	i32 287, ; 329
	i32 250, ; 330
	i32 306, ; 331
	i32 131, ; 332
	i32 75, ; 333
	i32 66, ; 334
	i32 172, ; 335
	i32 202, ; 336
	i32 143, ; 337
	i32 106, ; 338
	i32 151, ; 339
	i32 70, ; 340
	i32 156, ; 341
	i32 307, ; 342
	i32 176, ; 343
	i32 121, ; 344
	i32 127, ; 345
	i32 152, ; 346
	i32 225, ; 347
	i32 141, ; 348
	i32 298, ; 349
	i32 212, ; 350
	i32 20, ; 351
	i32 14, ; 352
	i32 135, ; 353
	i32 75, ; 354
	i32 59, ; 355
	i32 192, ; 356
	i32 215, ; 357
	i32 167, ; 358
	i32 168, ; 359
	i32 186, ; 360
	i32 15, ; 361
	i32 74, ; 362
	i32 6, ; 363
	i32 23, ; 364
	i32 237, ; 365
	i32 304, ; 366
	i32 196, ; 367
	i32 91, ; 368
	i32 1, ; 369
	i32 136, ; 370
	i32 238, ; 371
	i32 260, ; 372
	i32 134, ; 373
	i32 69, ; 374
	i32 146, ; 375
	i32 278, ; 376
	i32 229, ; 377
	i32 182, ; 378
	i32 88, ; 379
	i32 96, ; 380
	i32 219, ; 381
	i32 224, ; 382
	i32 31, ; 383
	i32 45, ; 384
	i32 233, ; 385
	i32 198, ; 386
	i32 109, ; 387
	i32 158, ; 388
	i32 35, ; 389
	i32 22, ; 390
	i32 114, ; 391
	i32 57, ; 392
	i32 258, ; 393
	i32 144, ; 394
	i32 118, ; 395
	i32 120, ; 396
	i32 110, ; 397
	i32 200, ; 398
	i32 139, ; 399
	i32 206, ; 400
	i32 54, ; 401
	i32 105, ; 402
	i32 187, ; 403
	i32 188, ; 404
	i32 133, ; 405
	i32 272, ; 406
	i32 263, ; 407
	i32 251, ; 408
	i32 229, ; 409
	i32 190, ; 410
	i32 308, ; 411
	i32 159, ; 412
	i32 285, ; 413
	i32 216, ; 414
	i32 163, ; 415
	i32 132, ; 416
	i32 251, ; 417
	i32 161, ; 418
	i32 298, ; 419
	i32 240, ; 420
	i32 140, ; 421
	i32 263, ; 422
	i32 259, ; 423
	i32 284, ; 424
	i32 169, ; 425
	i32 189, ; 426
	i32 201, ; 427
	i32 268, ; 428
	i32 40, ; 429
	i32 303, ; 430
	i32 227, ; 431
	i32 81, ; 432
	i32 56, ; 433
	i32 37, ; 434
	i32 97, ; 435
	i32 166, ; 436
	i32 172, ; 437
	i32 264, ; 438
	i32 82, ; 439
	i32 203, ; 440
	i32 98, ; 441
	i32 30, ; 442
	i32 159, ; 443
	i32 18, ; 444
	i32 127, ; 445
	i32 173, ; 446
	i32 119, ; 447
	i32 223, ; 448
	i32 254, ; 449
	i32 236, ; 450
	i32 256, ; 451
	i32 165, ; 452
	i32 231, ; 453
	i32 313, ; 454
	i32 253, ; 455
	i32 244, ; 456
	i32 170, ; 457
	i32 16, ; 458
	i32 144, ; 459
	i32 291, ; 460
	i32 125, ; 461
	i32 118, ; 462
	i32 38, ; 463
	i32 115, ; 464
	i32 47, ; 465
	i32 142, ; 466
	i32 117, ; 467
	i32 34, ; 468
	i32 175, ; 469
	i32 95, ; 470
	i32 53, ; 471
	i32 245, ; 472
	i32 129, ; 473
	i32 153, ; 474
	i32 24, ; 475
	i32 161, ; 476
	i32 222, ; 477
	i32 148, ; 478
	i32 104, ; 479
	i32 89, ; 480
	i32 210, ; 481
	i32 60, ; 482
	i32 142, ; 483
	i32 100, ; 484
	i32 5, ; 485
	i32 13, ; 486
	i32 191, ; 487
	i32 122, ; 488
	i32 135, ; 489
	i32 28, ; 490
	i32 72, ; 491
	i32 220, ; 492
	i32 24, ; 493
	i32 208, ; 494
	i32 249, ; 495
	i32 246, ; 496
	i32 137, ; 497
	i32 193, ; 498
	i32 201, ; 499
	i32 217, ; 500
	i32 168, ; 501
	i32 250, ; 502
	i32 101, ; 503
	i32 123, ; 504
	i32 221, ; 505
	i32 178, ; 506
	i32 163, ; 507
	i32 167, ; 508
	i32 224, ; 509
	i32 39, ; 510
	i32 185, ; 511
	i32 290, ; 512
	i32 17, ; 513
	i32 173, ; 514
	i32 171, ; 515
	i32 303, ; 516
	i32 137, ; 517
	i32 150, ; 518
	i32 213, ; 519
	i32 155, ; 520
	i32 130, ; 521
	i32 19, ; 522
	i32 65, ; 523
	i32 309, ; 524
	i32 147, ; 525
	i32 47, ; 526
	i32 283, ; 527
	i32 199, ; 528
	i32 79, ; 529
	i32 61, ; 530
	i32 282, ; 531
	i32 106, ; 532
	i32 248, ; 533
	i32 203, ; 534
	i32 0, ; 535
	i32 49, ; 536
	i32 234, ; 537
	i32 245, ; 538
	i32 14, ; 539
	i32 300, ; 540
	i32 177, ; 541
	i32 68, ; 542
	i32 288, ; 543
	i32 171, ; 544
	i32 209, ; 545
	i32 213, ; 546
	i32 296, ; 547
	i32 312, ; 548
	i32 78, ; 549
	i32 218, ; 550
	i32 108, ; 551
	i32 202, ; 552
	i32 244, ; 553
	i32 67, ; 554
	i32 63, ; 555
	i32 27, ; 556
	i32 160, ; 557
	i32 195, ; 558
	i32 211, ; 559
	i32 10, ; 560
	i32 185, ; 561
	i32 11, ; 562
	i32 78, ; 563
	i32 126, ; 564
	i32 83, ; 565
	i32 179, ; 566
	i32 66, ; 567
	i32 107, ; 568
	i32 65, ; 569
	i32 128, ; 570
	i32 122, ; 571
	i32 191, ; 572
	i32 77, ; 573
	i32 259, ; 574
	i32 249, ; 575
	i32 311, ; 576
	i32 8, ; 577
	i32 217, ; 578
	i32 2, ; 579
	i32 44, ; 580
	i32 262, ; 581
	i32 156, ; 582
	i32 128, ; 583
	i32 247, ; 584
	i32 23, ; 585
	i32 133, ; 586
	i32 205, ; 587
	i32 236, ; 588
	i32 306, ; 589
	i32 288, ; 590
	i32 29, ; 591
	i32 204, ; 592
	i32 62, ; 593
	i32 187, ; 594
	i32 90, ; 595
	i32 87, ; 596
	i32 292, ; 597
	i32 148, ; 598
	i32 189, ; 599
	i32 36, ; 600
	i32 86, ; 601
	i32 225, ; 602
	i32 301, ; 603
	i32 299, ; 604
	i32 296, ; 605
	i32 178, ; 606
	i32 50, ; 607
	i32 6, ; 608
	i32 90, ; 609
	i32 21, ; 610
	i32 162, ; 611
	i32 96, ; 612
	i32 50, ; 613
	i32 113, ; 614
	i32 241, ; 615
	i32 130, ; 616
	i32 76, ; 617
	i32 27, ; 618
	i32 301, ; 619
	i32 218, ; 620
	i32 240, ; 621
	i32 7, ; 622
	i32 186, ; 623
	i32 174, ; 624
	i32 110, ; 625
	i32 241, ; 626
	i32 227 ; 627
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nofree norecurse nosync nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: "no-trapping-math"="true" noreturn nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nofree norecurse nosync nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { "no-trapping-math"="true" noreturn nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.4xx @ df9aaf29a52042a4fbf800daf2f3a38964b9e958"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"min_enum_size", i32 4}
