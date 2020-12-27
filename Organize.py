import os
import shutil

dictionary = {
    "pdf": "Pdf",
    "xslx": "Docs",
    "docx": "Docs",
    "doc": "Docs",
    "rar": "Winrar",
    "zip": "Winrar",
    "jfif": "Images",
    "png": "Images",
    "jpg": "Images",
    "jpeg": "Images",
    "msi": "Apps",
    "exe": "Apps",
    "apk": "Apps",
    "xapk": "Apps",
    "torrent": "Uttorent Files",
    "mp3": "Music",
    "m4a": "Music",
}


def deplacer(filename, fileDest):
    if filename in os.listdir(fileDest):
        newFile = str(1) + file
        os.rename(filename, newFile)
        shutil.move(newFile, fileDest)
    else:
        shutil.move(filename, fileDest)


def check(file, filename, folderName):
    if os.path.exists(filename + "\\" + folderName) == False:
        os.mkdir(filename + "\\" + folderName)
        deplacer(file, dest + folderName)
    else:
        deplacer(file, dest + folderName)


filename = input("Directory path: ")
files = os.listdir(filename)

if len(files) != 0:
    for file in files:
        file = filename + "\\" + file
        dest = filename + "\\"
        checkExt = file.split(".")[-1]
        if checkExt in dictionary:
            check(file, filename, dictionary[checkExt])
        if checkExt in ["mp4", "webm"]:
            # st_size --> filesize in megabytes
            fileSize = os.stat(file).st_size
            if fileSize >= 200000000:
                check(file, filename, "Watch")
            else:
                check(file, filename, "Video")
print("Done...")