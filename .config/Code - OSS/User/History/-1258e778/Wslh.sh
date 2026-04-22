#!/usr/bin/env bash

# Путь к твоей новой теме
theme="$HOME/.config/rofi/powermenu.rasi"

# Иконки
shutdown=""
reboot="󰜉"
lock="󰌾"
logout="󰍃"
cancel="󰅖"

# Вызов основного меню
chosen=$(echo -e "$shutdown\n$reboot\n$lock\n$logout\n$cancel" | rofi -dmenu -config "$theme" -p "Power")

case $chosen in
    $shutdown)
        # Подтверждение тоже через ту же тему
        ans=$(echo -e "\n󰅖" | rofi -dmenu -config "$theme" -p "Confirm?")
        [ "$ans" == "" ] && shutdown now
        ;;
    $reboot)
        ans=$(echo -e "\n󰅖" | rofi -dmenu -config "$theme" -p "Confirm?")
        [ "$ans" == "" ] && reboot
        ;;
    $lock)
        hyprlock
        ;;
    $logout)
        ans=$(echo -e "\n󰅖" | rofi -dmenu -config "$theme" -p "Confirm?")
        [ "$ans" == "" ] && hyprctl dispatch exit
        ;;
esac