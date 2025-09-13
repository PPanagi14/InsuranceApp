import dayjs from "dayjs";
import isBetween from "dayjs/plugin/isBetween";

dayjs.extend(isBetween);

export const formatDateTime = (dt) => {
  if (!dt) return "";
  return dayjs(dt).format("DD MMM YYYY HH:mm");
};

export const formatDate = (dt) => {
  if (!dt) return "";
  return dayjs(dt).format("DD MMM YYYY");
};

export const isExpiringSoon = (dt, days = 30) => {
  if (!dt) return false;
  const end = dayjs(dt);
  const now = dayjs();
  return end.isBetween(now, now.add(days, "day"), null, "[)");
};