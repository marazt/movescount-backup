
$BASE_URL = "https://uiservices.movescount.com"
$EXPORT_URL = "http://www.movescount.com/move/export"
$APP_KEY = "SET_ME"
$USER_EMAIL = "SET_ME"
$USER_KEY = "SET_ME"
$COOKIE_VALUE = "SET_ME"
$AUTH_QUERY = "appkey=$APP_KEY&email=$USER_EMAIL&userkey=$USER_KEY"
$ROOT_DIR = "./MovescountBackup"

<#
.SYNOPSIS
Downloads data and store them.

.DESCRIPTION
Downloads data and store them.

.PARAMETER urlPart
Url part.

.PARAMETER filePath
Name of the file where to store downloaded data.

.EXAMPLE
GetAndStoreData "$($moveResponse.TrackURI)?$AUTH_QUERY" "$ROOT_DIR/$($moveId)/track_data.json"
#>
function GetAndStoreData([string]$urlPart, [string]$fileName)
{
    $url = "$BASE_URL/$urlPart"
    $response = Invoke-RestMethod -Uri $url
    Add-Content $fileName ($response | ConvertTo-Json)
    return $response
}

<#
.SYNOPSIS
Downloads GPS data and store them.

.DESCRIPTION
Downloads GPS data and store them.

.PARAMETER moveId
ID of the move.

.PARAMETER format
GPS data format.

.PARAMETER cookieValue
Value of the cookie needed for download.

.EXAMPLE
GetAndStoreGPSData $moveId "fit" $COOKIE_VALUE
#>
function GetAndStoreGPSData([long]$moveId, [string]$format, [string]$cookieValue)
{
    $url = "$EXPORT_URL?id=$($moveId)&format=$format"
    Invoke-WebRequest -Uri $url -OutFile "$ROOT_DIR/$($moveId)/gps_data.$format" -Headers @{"cookie"="$cookieValue"}
}

<#
.SYNOPSIS
Downloads move.

.DESCRIPTION
Downloads move.

.PARAMETER moveId
ID of the move.

.EXAMPLE
GetAndStoreGPSData 61859797
#>
function GetMove([long]$moveId)
{
    Write-Output "Processing of move $moveId started"
    New-Item -ItemType Directory -Force -Path "$ROOT_DIR/$($moveId)"
    New-Item -ItemType Directory -Force -Path "$ROOT_DIR/$($moveId)/media"
    
    $moveResponse = GetAndStoreData "moves/$($moveId)?$AUTH_QUERY" "$ROOT_DIR/$($moveId)/move_data.json"
    GetAndStoreData "$($moveResponse.TrackURI)?$AUTH_QUERY" "$ROOT_DIR/$($moveId)/track_data.json"
    GetAndStoreData "$($moveResponse.MarksURI)?$AUTH_QUERY" "$ROOT_DIR/$($moveId)/marks_data.json"
    GetAndStoreData "$($moveResponse.SamplesURI)?$AUTH_QUERY" "$ROOT_DIR/$($moveId)/samples_data.json"
    $mediaResourcesResponse = GetAndStoreData "$($moveResponse.MediaResourcesURI)?$AUTH_QUERY" "$ROOT_DIR/$($moveId)/media_data.json"
    GetAndStoreGPSData $moveId "kml" $COOKIE_VALUE
    GetAndStoreGPSData $moveId "gpx" $COOKIE_VALUE
    GetAndStoreGPSData $moveId "xlsx" $COOKIE_VALUE
    GetAndStoreGPSData $moveId "fit" $COOKIE_VALUE
    GetAndStoreGPSData $moveId "tcx" $COOKIE_VALUE

    if ($mediaResourcesResponse) 
    {
        Foreach ($media in $mediaResourcesResponse)
        {
            Write-Output "Processing media $media.URIOriginal"
            $parts = $media.URIOriginal.Split("/")
            Invoke-WebRequest -Uri $media.URIOriginal -OutFile "$ROOT_DIR/$($moveId)/media/$($parts[$parts.Lenght - 1])"
        }
    }

    Write-Output "Processing of move $moveId done"
}
