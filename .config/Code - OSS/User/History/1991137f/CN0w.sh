#!/usr/bin/env bash

# Пути к папкам
# Темы лежат здесь: ~/.config/hypr/themes/имя_темы/.config/...
STOWDIR="$HOME/.config/hypr/themes"
STOWTARGET="$HOME"
COREPKG="c.c"
RELOADSCRIPT="$HOME/.config/scripts/reload.sh"

# 1. Получаем список только папок с темами (исключая COREPKG)
pkglist=$(ls -1 "$STOWDIR" | grep -v "^$COREPKG$")

# 2. Проверка, нашлись ли темы
if [[ -z "$pkglist" ]]; then
    echo "DEBUG: No themes found in $STOWDIR"
    exit 1
fi

# 3. Выбор темы через Rofi
# -no-config отключает твой сломанный конфиг, используем только лаймовый hyprmed.rasi
selection=$(echo "$pkglist" | rofi -dmenu -i -p "Select Theme" \
    -no-config \
    -theme "$HOME/.config/rofi/themes/hyprmed.rasi")

# Если нажали ESC — выходим
[[ -z "$selection" ]] && exit 0

echo "Switching to: $selection"

# 4. Переключение тем
# Сначала удаляем симлинки всех существующих тем, чтобы не было конфликтов
for pkg in $(ls -1 "$STOWDIR" | grep -v "^$COREPKG$"); do
  stow --dir "$STOWDIR" --target "$STOWTARGET" --delete "$pkg" 2>/dev/null
done

# Stow'им выбранную тему и "ядро" (COREPKG)
# Важно: stow будет брать содержимое папки ~/.config/hypr/themes/выбранная_тема/
# и переносить его в $HOME (то есть в ~/.config/...)
stow --dir "$STOWDIR" --target "$STOWTARGET" --stow "$selection"
stow --dir "$STOWDIR" --target "$STOWTARGET" --restow "$COREPKG"

# 5. Перезагрузка системы
if [[ -f "$RELOADSCRIPT" ]]; then
    "$RELOADSCRIPT"
else
    echo "Error: reload.sh not found!"
fi

echo "Success!"